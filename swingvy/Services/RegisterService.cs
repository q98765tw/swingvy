using swingvy.Enums;
using swingvy.Models;
using swingvy.Repositories;
using System.Security.Cryptography;
using System.Text;

public class RegisterService
{
    private readonly IMemberRepository _memberRepository;
    private readonly ICalendarRepository _calendarRepository;
    //private readonly MemberRepository _memberRepository;
    //private readonly CalendarRepository _calendarRepository;

    public RegisterService(IMemberRepository memberRepository, ICalendarRepository calendarRepository)
    {
        _memberRepository = memberRepository;
        _calendarRepository = calendarRepository;
    }

    public bool RegisterUser(string account, string password)
    {
        try
        {
            // 生成隨機盐值
            byte[] salt = GenerateSalt();

            // 計算密碼哈希值
            byte[] passwordHash = ComputeHash(password, salt);
            // 創建新用戶
            var newUser = new member
            {
                account = account,
                password = password,
                PasswordHash = passwordHash,
                Salt = salt
            };

            _memberRepository.AddUser(newUser);

            // 其他相關業務邏輯，例如創建行事曆等
            var CalNewMember = new calendar
            {
                member_id = newUser.member_id,
                startTime = DateTime.Now,
                endTime = DateTime.Now,
                name = "到職"
            };
            _calendarRepository.AddCalendar(CalNewMember);
            return true;
        }
        catch
        {
            return false;
        }
       
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[64]; // 64字节的盐值
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private byte[] ComputeHash(string password, byte[] salt)
    {
        using (var hmac = new HMACSHA512(salt))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            return hmac.ComputeHash(passwordBytes);
        }
    }
}
