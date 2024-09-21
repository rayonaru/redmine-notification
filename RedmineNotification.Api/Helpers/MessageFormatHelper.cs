using System.Text;
using System.Text.RegularExpressions;
using RedmineNotification.Core.Models.Payload;

namespace RedmineNotification.Api.Helpers;

public static class MessageFormatHelper
{
    public static string FormatPayloadMessage(Payload? payload)
    {
        if (payload is null)
            return string.Empty;

        var issue = payload.Issue;
        var project = issue?.Project;
        var author = issue?.Author;
        var assignee = issue?.Assignee;
        var watchers = issue?.Watchers;

        var header = payload.Action == "opened" ? "Появилась новая задача" : "Новая информация по задаче";

        var watchersNames = watchers?.Count != 0
            ? $"- *Контроль качества:* {string.Join(", ", watchers!.Select(x => $"{x.Firstname} {x.Lastname}"))}"
            : string.Empty;

        var comment = payload.Journal?.Notes?.Length >= 1
            ? $@"
🖊*Новый комментарий* 
- *Автор:* {payload?.Journal?.Author?.Firstname} {payload?.Journal?.Author?.Lastname}
- *Комментарий:* {payload?.Journal?.Notes}
- *Время:* {payload?.Journal?.CreatedOn}
"
            : string.Empty;

        var message = $@"
📎 *{header}*

📝*Детали задачи*
- *Проект:* *{project?.Name}*
- *Трекер:* {issue?.Tracker?.Name}
- *Номер:* #{issue?.Id}
- *Проблема:* {FormatLinksInText(issue?.Subject)}
- *Описание:* {FormatLinksInText(issue?.Description)}
- *Статус:* {issue?.Status?.Name}
- *Приоритет:* *{issue?.Priority?.Name}*

🤼‍♂️*Участники*
- *Автор:* {author?.Firstname} {author?.Lastname}
- *Исполнитель:* {assignee?.Firstname} {assignee?.Lastname}
{watchersNames}
{FormatLinksInText(comment)}
🔖*Дополнительно*
- *Время последнего обновления:* {issue?.UpdatedOn}
- [Ссылка на задачу]({payload?.Url})";

        return message;
    }

    private static string FormatLinksInText(string? message)
    {
        if (message is null)
            return string.Empty;

        return Regex.Replace(message, @"http[^\s]+", match =>
        {
            var url = match.Value;
            var escapedUrl = Uri.EscapeUriString(url);
            return $"[{escapedUrl}]({url})";
        });
    }

    private static string EscapeMarkdown(string? text)
    {
        if (text is null)
            return string.Empty;

        var reservedChars = new[]
            { '_', '*', '[', ']', '(', ')', '~', '`', '>', '#', '+', '-', '=', '|', '{', '}', '.', '!' };
        var escapedText = new StringBuilder();

        foreach (var c in text)
        {
            if (reservedChars.Contains(c))
                escapedText.Append('\\');
            escapedText.Append(c);
        }

        return escapedText.ToString();
    }
}
