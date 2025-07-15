namespace GlobalFileStorageSystem.Domain.Enums
{
    [Flags]
    public enum ComplianceRequirement
    {
        None = 0,
        GDPR = 1 << 0, // 1
        HIPAA = 1 << 1, // 2
        SOX = 1 << 2, // 4
        PCI_DSS = 1 << 3  // 8
    }
}
