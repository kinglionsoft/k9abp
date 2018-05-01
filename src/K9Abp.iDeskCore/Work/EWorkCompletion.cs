namespace K9Abp.iDeskCore.Work
{
    public enum EWorkCompletion
    {
        未完成 = 1,
        按时完成 = 2,
        超时完成 = 4,
        严重超时 = 8,
        超时 = 超时完成 | 严重超时 
    }
}