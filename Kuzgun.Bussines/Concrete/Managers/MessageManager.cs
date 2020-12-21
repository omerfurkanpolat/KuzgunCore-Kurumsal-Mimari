using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Bussines.Abstract;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class MessageManager:IMessageService
    {
        private IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public List<Message> GetAll()
        {
            return _messageDal.GetList();
        }

        public Message GetById(int id)
        {
            return _messageDal.Get(m => m.MessageId == id);
        }

        public void Create(Message entity)
        {
            _messageDal.Add(entity);
        }

        public void Update(Message entity)
        {
            _messageDal.Update(entity);
        }

        public void Delete(Message entity)
        {
            _messageDal.Delete(entity);
        }
    }
}
