using ITventory.Domain.AbstractClasses;
using ITventory.Domain.Enums;
using ITventory.Domain;
using System.Collections.ObjectModel;

public class Hardware : Item
{
    public Guid Id { get; private set; }
    public Guid PrimaryUserId { get; private set; }

    public Guid? TopUser =>
        _historyOfLogons
        .GroupBy(l => l.UserId)
        .OrderByDescending(g => g.Count())
        .FirstOrDefault()
        ?.Key;

    public Region DefaultDomain { get; private set; }
    public HardwareType HardwareType { get; init; }
    public bool IsActive { get; private set; }
    public int LoginCount => _historyOfLogons.Count;

    public List<Logon> _historyOfLogons = new List<Logon>();
    //public IReadOnlyCollection<Logon> HistoryOfLogons => (_historyOfLogons as List<Logon>).AsReadOnly();

    private Hardware() { }

    public Hardware(
        Guid primaryUserId,
        Region defaultDomain,
        HardwareType hardwareType,
        string description,
        double worth,
        Guid producentId,
        Guid modelId,
        int modelYear,
        string serialNumber,
        DateOnly purchasedDate,
        Guid roomId,
        Guid departmentId
    ) : base(description, worth, producentId, modelId, modelYear, serialNumber, purchasedDate, roomId, departmentId)
    {
        Id = Guid.NewGuid();
        PrimaryUserId = primaryUserId;
        DefaultDomain = defaultDomain;
        HardwareType = hardwareType;
        IsActive = true;
    }

    public static Hardware Create(
        Guid primaryUserId,
        Region defaultDomain,
        HardwareType hardwareType,
        string description,
        double worth,
        Guid producentId,
        Guid modelId,
        int modelYear,
        string serialNumber,
        DateOnly purchasedDate,
        Guid roomId,
        Guid departmentId
    )
    {
        return new Hardware(
            primaryUserId,
            defaultDomain,
            hardwareType,
            description,
            worth,
            producentId,
            modelId,
            modelYear,
            serialNumber,
            purchasedDate,
            roomId,
            departmentId
        );
    }

    public void AddLogon(Logon logon)
    {
        if (logon == null)
        {
            throw new ArgumentNullException(nameof(logon));
        }

        if (_historyOfLogons.Any(l => l.Id == logon.Id))
        {
            throw new ArgumentException("This logon instance already exists");
        }

        _historyOfLogons.Add(logon);
    }

    public void SetPrimaryUser(Employee user)
    {
        if (user == null)
        {
            throw new ArgumentNullException("User cannot be empty");
        }

        PrimaryUserId = user.Id;
    }
}
