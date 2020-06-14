using System;
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
    }
}