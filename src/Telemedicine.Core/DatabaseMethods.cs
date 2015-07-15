using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TelemedicineDAL.Common;
using TelemedicineDAL.Daos;
using TelemedicineDAL.Entities;

namespace Telemedicine.Core
{
    public static class DatabaseMethods
    {
        #region Private

        private static readonly Lazy<UserDao> _userDao = new Lazy<UserDao>(() => new UserDao(), LazyThreadSafetyMode.ExecutionAndPublication);
        private static readonly Lazy<DoctorDao> _doctorDao = new Lazy<DoctorDao>(() => new DoctorDao(), LazyThreadSafetyMode.ExecutionAndPublication);
        private static readonly Lazy<PatientDao> _patientDao = new Lazy<PatientDao>(() => new PatientDao(), LazyThreadSafetyMode.ExecutionAndPublication);
        private static readonly Lazy<ChatDao> _chatDao = new Lazy<ChatDao>(() => new ChatDao(), LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion

        #region Public

        /// <summary>
        ///     DAO пользователей
        /// </summary>
        public static UserDao UserDao
        {
            get { return _userDao.Value; }
        }

        /// <summary>
        ///     DAO врачей
        /// </summary>
        public static DoctorDao DoctorDao
        {
            get { return _doctorDao.Value; }
        }

        /// <summary>
        ///     DAO пациентов
        /// </summary>
        public static PatientDao PatientDao
        {
            get { return _patientDao.Value; }
        }

        /// <summary>
        ///     DAO чатов
        /// </summary>
        public static ChatDao ChatDao
        {
            get { return _chatDao.Value; }
        }

        #endregion

        /// <summary>
        ///     Выполняет аутентификацию пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Результат аутентификации</returns>
        public static AuthResult Auth(string login, string password)
        {
            return UserDao.Auth(login, password);
        }

        /// <summary>
        ///     Возвращает пользователя с указанным логином
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Пользователь, либо null, если пользователь с указанным логином не существует</returns>
        public static User GetUserByLogin(string login)
        {
            return UserDao.Select(login);
        }

        /// <summary>
        ///     Возвращает врача с указанным логином
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Врач, либо null, если врач с указанным логином не существует</returns>
        public static Doctor GetDoctorByLogin(string login)
        {
            return DoctorDao.Select(login);
        }

        /// <summary>
        ///     Возвращает пациента с указанным логином
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Паиент, либо null, если пациент с указанным логином не существует</returns>
        public static Patient GetPatientByLogin(string login)
        {
            return PatientDao.Select(login);
        }

        /// <summary>
        /// Выполняет отправку текстового сообщения
        /// </summary>
        /// <param name="doctorLogin">Логин врача</param>
        /// <param name="patientLogin">Логин пациента</param>
        /// <param name="messageData">Данные сообщения</param>
        /// <param name="chatMessageType">Тип сообщения. Определяет адресата и отправителя</param>
        /// <returns>Идентификатор созданного сообщения</returns>
        public static Guid SendChatMessage(string doctorLogin, string patientLogin, string messageData, MessageType chatMessageType)
        {
            return ChatDao.SendMessage(doctorLogin, patientLogin, chatMessageType, messageData);
        }

        /// <summary>
        /// Возвращает чат между врачом и пациентом
        /// </summary>
        /// <param name="doctorLogin">Логин врача</param>
        /// <param name="patientLogin">Логин пациента</param>
        /// <returns>Чат вместе со всеми сообщениями</returns>
        public static Chat GetChat(string doctorLogin, string patientLogin)
        {
            return ChatDao.GetOrCreateChat(doctorLogin, patientLogin);
        }

        /// <summary>
        /// Возвращает всех зарегистрированных в системе врачей
        /// </summary>
        /// <returns>Словарь [логин, данные_врача]</returns>
        public static Dictionary<string, Doctor> GetAllDoctors()
        {
            return DoctorDao.GetAllDoctors();
        }

        /// <summary>
        /// Попытка зарегистрировать пользователя-пациента
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="patient">Данные рациента</param>
        /// <returns>Результат регистрации</returns>
        public static RegistrationResult RegisterPatient(string login, string password, Patient patient)
        {
            return UserDao.RegisterPatient(login, password, patient);
        }

        /// <summary>
        /// Попытка зарегистрировать пользователя-врача
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="doctor">Данные врача</param>
        /// <returns>Результат регистрации</returns>
        public static RegistrationResult RegisterDoctor(string login, string password, Doctor doctor)
        {
            return UserDao.RegisterDoctor(login, password, doctor);
        }
    }
}