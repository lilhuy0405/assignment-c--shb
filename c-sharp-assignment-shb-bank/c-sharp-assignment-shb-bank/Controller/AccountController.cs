using System;
using System.Security.Cryptography;
using c_sharp_assignment_shb_bank.Entity;
using c_sharp_assignment_shb_bank.Helper;
using c_sharp_assignment_shb_bank.Model;

namespace c_sharp_assignment_shb_bank.Controller
{
    public class AccountController
    {
        private PasswordHelper _passwordHelper = new PasswordHelper();
        private AccountModel _accountModel = new AccountModel();

        public Account Login()
        {
            Console.WriteLine("-------Đăng nhập-------");
            Console.WriteLine("Xin mời nhập username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Xin mời nhập mật khẩu: ");
            var password = Console.ReadLine();
            var account = _accountModel.GetActiveAccountByUsername(username);

            // mã hóa pass người dùng nhập vào kèm theo muối trong database và so sánh kết quả với password đã được mã hóa trong database.
            if (account != null && _passwordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
            {
                return account;
            }

            return null;
        }

        public void Register()
        {
            var newAccount = new Account()
            {
                Balance = 0 ,
                Status = AccountStatus.ACTIVE ,
                Salt = _passwordHelper.GenerateSalt(),
                AccountNumber = CreatedUniqueRamdomAccountNumber(),
                Role =   AccountRole.GUEST
                
            };
            Console.WriteLine("--Register--");
            Console.WriteLine("Enter Username");
            newAccount.Username = Console.ReadLine();
            Console.WriteLine("Enter password");
            var password = Console.ReadLine();
            Console.WriteLine("Enter your full name");
            newAccount.FullName = Console.ReadLine();
            Console.WriteLine("Enter your email");
            newAccount.Email = Console.ReadLine();
            Console.WriteLine("Enter your phone number");
            newAccount.PhoneNumber = Console.ReadLine();
            
            _passwordHelper.ComparePassword(password, newAccount.Salt, newAccount.PasswordHash);

            _accountModel.SaveAccount(newAccount);

        }

        private string CreatedUniqueRamdomAccountNumber()
        {
            var random = new Random();
            var ramdomNumber = "0000000000";
            int i;
            for (i = 1; i < 11; i++)
            {
                ramdomNumber += random.Next(0, 9).ToString();
            }
            

            while (!_accountModel.CheckExsitAccountNumber(ramdomNumber))
            {
                for (i = 1; i < 11; i++)
                {
                    ramdomNumber += random.Next(0, 9).ToString();
                }
            }

            return ramdomNumber;
        }
    }
}