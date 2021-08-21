using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public class Messages
    {
        public static string Added = "Ekleme başarılı";
        public static string Updated = "Güncelleme başarılı";
        public static string Deleted = "Silme başarılı";
        public static string Listed = "Listeleme başarılı";
        public static string Geted = "Getirme başarılı";
        public static string ProductLimitExceeded = "Bu kategorideki ürün sınırı aşıldı";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string WrongPassword = "Şifre yanlış";
        public static string LoginSuccess = "Giriş başarılı";
        public static string RegisterSuccess = "Kayıt başarılı";
        public static string UserAlreadyExist ="Kullanıcı zaten var";
        public static string TokenCreated = "Token oluşturuldu";
        
    }
}
