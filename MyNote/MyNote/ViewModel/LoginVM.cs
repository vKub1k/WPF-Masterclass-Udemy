using System;
using MyNote.Model;
using MyNote.ViewModel.Commands;

namespace MyNote.ViewModel;

public class LoginVM
{
    private User _user;

    public User User
    {
        get => _user;
        set => _user = value ?? throw new ArgumentNullException(nameof(value));
    }

    public RegisterCommand RegisterCommand { get; set; }
    public LoginCommand LoginCommand { get; set; }

    public LoginVM()
    {
        RegisterCommand = new RegisterCommand(this);
        LoginCommand = new LoginCommand(this);
    }
}