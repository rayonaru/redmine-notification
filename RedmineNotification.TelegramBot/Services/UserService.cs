using Newtonsoft.Json;
using RedmineNotification.Core.Exceptions;
using RedmineNotification.TelegramBot.Models;

namespace RedmineNotification.TelegramBot.Services;

public class UserService
{
    private readonly List<User>? _botUsers;
    private readonly string _usersJsonPath;

    public UserService(string? usersJsonPath)
    {
        _usersJsonPath = usersJsonPath ?? throw new SimpleException("Path to users json file is null");
        _botUsers = ReadUsersFromFile(_usersJsonPath);
    }

    public bool UserExists(long chatId)
    {
        return _botUsers != null && _botUsers.Exists(x => x.ChatId == chatId);
    }

    public User? GetUser(long chatId)
    {
        return _botUsers?.Find(x => x.ChatId == chatId);
    }

    public IEnumerable<User> GetUsers(params string[]? redmineLogins)
    {
        if (redmineLogins == null || redmineLogins.Length == 0)
        {
            return Enumerable.Empty<User>();
        }

        var loginSet = new HashSet<string>(redmineLogins.Where(login => !string.IsNullOrEmpty(login)));

        return _botUsers?.Where(user => user.RedmineLogin != null && loginSet.Contains(user.RedmineLogin)) ?? Enumerable.Empty<User>();
    }

    public void AddUser(User user)
    {
        _botUsers?.Add(user);

        WriteUsersToFile(_usersJsonPath, _botUsers);
    }

    public void UpdateUser(User user)
    {
        WriteUsersToFile(_usersJsonPath, _botUsers);
    }

    private static List<User>? ReadUsersFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<User>();
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<User>>(json);
    }

    private static void WriteUsersToFile(string filePath, List<User>? users)
    {
        if (users is null || !users.Any())
            return;

        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
