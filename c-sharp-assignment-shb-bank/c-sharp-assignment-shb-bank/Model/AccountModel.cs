using System;
using System.ComponentModel.DataAnnotations;
using c_sharp_assignment_shb_bank.Entity;
using c_sharp_assignment_shb_bank.Helper;
using MySql.Data.MySqlClient;

namespace c_sharp_assignment_shb_bank.Model
{
    public class AccountModel
    {
        /* model for account */
        
        public Account GetActiveAccountByUsername(string username)
        {
            // kết nối database
            Account account = null;
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"select * from account where username = '{username}' and status = '{(Enum) AccountStatus.ACTIVE}'",
                cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                account = new Account()
                {
                   FullName = reader.GetString("fullName"),
                   AccountNumber = reader.GetString("accountNumber"),
                   PhoneNumber = reader.GetString("phoneNumber"),
                   Email = reader.GetString("email"),
                   Salt = reader.GetString("salt"),
                   PasswordHash = reader.GetString("passwordHash"),
                   Username = reader.GetString("username"),
                   Role = (AccountRole) reader.GetInt32("role"),
                   Balance = reader.GetDouble("balance"),
                   Status = (AccountStatus) reader.GetInt32("status")
                };
            }

            cnn.Close();
            return account;
        }

        public Boolean CheckExsitAccountNumber(string ramdomNumber)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"select rollnumber from accounts where rollnumber = '{ramdomNumber}'",cnn);

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            return false;
        }

        public void SaveAccount(Account newAccount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"insert into accounts values('{newAccount.AccountNumber}','{newAccount.Username}','{newAccount.PasswordHash}','{newAccount.Salt}','{newAccount.FullName}','{newAccount.PhoneNumber}','{newAccount.Email}','{newAccount.Balance}','{newAccount.Status}','{newAccount.Role}',) ");
            cmd.ExecuteNonQuery();
            Console.WriteLine("Created new account success");
        }
    }
}