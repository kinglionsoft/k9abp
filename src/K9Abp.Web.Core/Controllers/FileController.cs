using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using K9Abp.Application.Dto;
using K9Abp.Core;
using K9Abp.Core.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace K9Abp.Web.Core.Controllers
{
    public class FileController : K9AbpControllerBase
    {
        private readonly IAppFolders _appFolders;

        public FileController(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        [DisableAuditing]
        public ActionResult DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(fileBytes, file.FileType, file.FileName);
        }

        [DisableAuditing]
        public async Task<string> ImportOu()
        {
            var org = new Dictionary<string, long>();

            var manager = Request.HttpContext.RequestServices.GetService<OrganizationUnitManager>();
            var repository = Request.HttpContext.RequestServices.GetService<IRepository<OrganizationUnit, long>>();
            using (var reader = new StreamReader(@"d:\channel.txt", Encoding.UTF8))
            {
                reader.ReadLine(); // skip 1 row;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var parts = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                    // county
                    var countyName = parts[0];
                    if (!org.ContainsKey(countyName))
                    {
                        var county = new OrganizationUnit(1, countyName);
                        await manager.CreateAsync(county);
                        CurrentUnitOfWork.SaveChanges();
                        org[countyName] = repository.Single(x => x.DisplayName == countyName).Id;
                    }
                    // distinct
                    var distinctName = parts[3];
                    if (!org.ContainsKey(distinctName))
                    {
                        var distinct = new OrganizationUnit(1, distinctName, org[countyName]);
                        await manager.CreateAsync(distinct);
                        CurrentUnitOfWork.SaveChanges();
                        org[distinctName] = repository.Single(x => x.DisplayName == distinctName).Id;
                    }
                    // channel
                    var channel = new OrganizationUnit(1, parts[1], org[distinctName]);
                    await manager.CreateAsync(channel);
                    CurrentUnitOfWork.SaveChanges();
                }
            }

            return "ok";
        }

        [DisableAuditing]
        public async Task<string> ImportUser()
        {
            // var userManager = Request.HttpContext.RequestServices.GetService<UserManager>();
            var userRep = Request.HttpContext.RequestServices.GetService<IRepository<User, long>>();
            // var ouRep = Request.HttpContext.RequestServices.GetService<IRepository<UserOrganizationUnit, long>>();
            var users = new List<User>();
            using (var reader = new StreamReader(@"d:\user.txt", Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var parts = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                    var user = new User
                    {
                        Name = parts[1],
                        UserName = parts[0],
                        Surname = parts[0].Substring(0,1),
                        EmailAddress = parts[0] + "@139.com",
                        CreatorUserId = 1,
                        CreationTime = DateTime.Today
                    };
                    user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, "123qwe");
                    user.SetNormalizedNames();
                    users.Add(user);
                }
            }

            await userRep.BulkInsertAsync(users);
            return "ok";
        }

    }
}
