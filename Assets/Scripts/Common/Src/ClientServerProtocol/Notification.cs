namespace Civ.Common.ClientServerProtocol {



public class Notification : IMessage
{
	public string ObjectId { get; }

	public NotificationType Type { get; }

	public INotificationPayload Payload { get; }
}



public enum NotificationType
{
	// Object
	Created,
	Changed,
	Deleted,

	// Unordered collection
	ItemCreated,
	ItemDeleted
}



public interface INotificationPayload {}


public class CreatedNotificationPayload : INotificationPayload
{
	public string ObjectId { get; }
}

public class ChangedNotificationPayload : INotificationPayload
{
	public string ObjectId { get; }
}

public class DeletedNotificationPayload : INotificationPayload
{
	public string ObjectId { get; }
}



}
