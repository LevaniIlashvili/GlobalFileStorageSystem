namespace GlobalFileStorageSystem.Domain.Enums
{
    [Flags]
    public enum Permission
    {
        None = 0,
        FileRead = 1 << 0, // 1
        FileWrite = 1 << 1, // 2
        FileDelete = 1 << 2, // 4
        FolderCreate = 1 << 3, // 8
        ShareCreate = 1 << 4, // 16
        MetadataEdit = 1 << 5, // 32
        UserManage = 1 << 6, // 64
        BillingView = 1 << 7, // 128
        AuditView = 1 << 8  // 256
    }
}
