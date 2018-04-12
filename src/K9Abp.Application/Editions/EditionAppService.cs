﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Editions;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using K9Abp.Application.Editions.Dto;
using K9Abp.Core.Authorization;
using K9Abp.Core.Editions;

namespace K9Abp.Application.Editions
{
    [AbpAuthorize(PermissionNames.Editions)]
    public class EditionAppService : K9AbpAppServiceBase, IEditionAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<SubscribableEdition> _subscribableEditionRepository;

        public EditionAppService(
            EditionManager editionManager,
            IRepository<SubscribableEdition> subscribableEditionRepository)
        {
            _editionManager = editionManager;
            _subscribableEditionRepository = subscribableEditionRepository;
        }

        public async Task<ListResultDto<EditionListDto>> GetEditions()
        {
            var editions = ObjectMapper.Map<List<SubscribableEdition>>(await _editionManager.Editions.ToListAsync())
                .OrderBy(e => e.MonthlyPrice);
            return new ListResultDto<EditionListDto>(
                ObjectMapper.Map<List<EditionListDto>>(editions)
                );
        }

        [AbpAuthorize(PermissionNames.Editions_Create, PermissionNames.Editions_Edit)]
        public async Task<GetEditionEditOutput> GetEditionForEdit(NullableIdDto input)
        {
            var features = FeatureManager.GetAll();

            EditionEditDto editionEditDto;
            List<NameValue> featureValues;

            if (input.Id.HasValue) //Editing existing edition?
            {
                var edition = await _editionManager.FindByIdAsync(input.Id.Value);
                featureValues = (await _editionManager.GetFeatureValuesAsync(input.Id.Value)).ToList();
                editionEditDto = ObjectMapper.Map<EditionEditDto>(edition);
            }
            else
            {
                editionEditDto = new EditionEditDto();
                featureValues = features.Select(f => new NameValue(f.Name, f.DefaultValue)).ToList();
            }

            var featureDtos = ObjectMapper.Map<List<FlatFeatureDto>>(features).OrderBy(f => f.DisplayName).ToList();

            return new GetEditionEditOutput
            {
                Edition = editionEditDto,
                Features = featureDtos,
                FeatureValues = featureValues.Select(fv => new NameValueDto(fv)).ToList()
            };
        }

        [AbpAuthorize(PermissionNames.Editions_Create, PermissionNames.Editions_Edit)]
        public async Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input)
        {
            if (!input.Edition.Id.HasValue)
            {
                await CreateEditionAsync(input);
            }
            else
            {
                await UpdateEditionAsync(input);
            }
        }

        [AbpAuthorize(PermissionNames.Editions_Delete)]
        public async Task DeleteEdition(EntityDto input)
        {
            var edition = await _editionManager.GetByIdAsync(input.Id);
            await _editionManager.DeleteAsync(edition);
        }

        public async Task<List<SubscribableEditionComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null, bool addAllItem = false, bool onlyFreeItems = false)
        {
            var editions = await _editionManager.Editions.ToListAsync();
            var subscribableEditions = ObjectMapper.Map<List<SubscribableEdition>>(editions)
                .WhereIf(onlyFreeItems, e => e.IsFree)
                .OrderBy(e => e.MonthlyPrice);

            var editionItems =
                new ListResultDto<SubscribableEditionComboboxItemDto>(subscribableEditions
                    .Select(e => new SubscribableEditionComboboxItemDto(e.Id.ToString(), e.DisplayName, e.IsFree)).ToList()).Items.ToList();

            var defaultItem = new SubscribableEditionComboboxItemDto("", L("NotAssigned"), null);
            editionItems.Insert(0, defaultItem);

            if (addAllItem)
            {
                editionItems.Insert(0, new SubscribableEditionComboboxItemDto("-1", "- " + L("All") + " -", null));
            }

            if (selectedEditionId.HasValue)
            {
                var selectedEdition = editionItems.FirstOrDefault(e => e.Value == selectedEditionId.Value.ToString());
                if (selectedEdition != null)
                {
                    selectedEdition.IsSelected = true;
                }
            }
            else
            {
                editionItems[0].IsSelected = true;
            }

            return editionItems;
        }

        [AbpAuthorize(PermissionNames.Editions_Create)]
        protected virtual async Task CreateEditionAsync(CreateOrUpdateEditionDto input)
        {
            var edition = ObjectMapper.Map<SubscribableEdition>(input.Edition);

            if (edition.ExpiringEditionId.HasValue)
            {
                var expiringEdition = ObjectMapper.Map<SubscribableEdition>(await _editionManager.GetByIdAsync(edition.ExpiringEditionId.Value));
                if (!expiringEdition.IsFree)
                {
                    throw new UserFriendlyException(L("ExpiringEditionMustBeAFreeEdition"));
                }
            }

            await _editionManager.CreateAsync(edition);
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the edition.

            await SetFeatureValues(edition, input.FeatureValues);
        }

        [AbpAuthorize(PermissionNames.Editions_Edit)]
        protected virtual async Task UpdateEditionAsync(CreateOrUpdateEditionDto input)
        {
            if (input.Edition.Id != null)
            {
                var edition = await _editionManager.GetByIdAsync(input.Edition.Id.Value);

                var existingSubscribableEdition = ObjectMapper.Map<SubscribableEdition>(edition);
                var updatingSubscribableEdition = ObjectMapper.Map<SubscribableEdition>(input.Edition);
                if (existingSubscribableEdition.IsFree &&
                    !updatingSubscribableEdition.IsFree &&
                    await _subscribableEditionRepository.CountAsync(e => e.ExpiringEditionId == existingSubscribableEdition.Id) > 0)
                {
                    throw new UserFriendlyException(L("ThisEditionIsUsedAsAnExpiringEdition"));
                }

                ObjectMapper.Map(input.Edition, edition);

                await SetFeatureValues(edition, input.FeatureValues);
            }
        }

        private Task SetFeatureValues(Edition edition, List<NameValueDto> featureValues)
        {
            return _editionManager.SetFeatureValuesAsync(edition.Id,
                featureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }
    }
}

