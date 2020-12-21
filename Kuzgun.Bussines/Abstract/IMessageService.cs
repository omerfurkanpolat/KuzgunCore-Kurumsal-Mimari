using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface IMessageService
    {
        List<Message> GetAll();
        Message GetById(int id);
        void Create(Message entity);
        void Update(Message entity);
        void Delete(Message entity);
    }
}
