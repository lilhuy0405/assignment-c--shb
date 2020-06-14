using System;
using c_sharp_assignment_shb_bank.Controller;
using c_sharp_assignment_shb_bank.Entity;

namespace c_sharp_assignment_shb_bank.View
{
    public class ConsoleView
    {
        private static Account currentLogin;

        public static void GenerateMenu()
        {
            // Menu khi người dùng mở ứng dụng

            var controller = new AccountController();
            while (currentLogin == null)
            {
                Console.WriteLine("-------Ngân hàng Spring Hero Bank-------");
                Console.WriteLine("1. Đăng ký tài khoản");
                Console.WriteLine("2. Đăng nhập hệ thống");
                Console.WriteLine("3. Thoát");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (1, 2, 3): ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        var account = controller.Login();
                        if (account == null)
                        {
                            Console.WriteLine("Login failed!");
                            return;
                        }

                        currentLogin = account;
                        Console.WriteLine($"Login success! Welcome back {currentLogin.FullName}");
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }
}