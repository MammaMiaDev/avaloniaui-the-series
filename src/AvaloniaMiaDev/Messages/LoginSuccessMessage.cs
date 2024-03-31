using AvaloniaMiaDev.Services;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AvaloniaMiaDev.Messages;

public class LoginSuccessMessage(AuthenticationResult result) : ValueChangedMessage<AuthenticationResult>(result);
