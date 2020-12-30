using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Core.Utilities.EmailService.Smtp;
using Kuzgun.Entities.ComplexTypes.MessagesDTO;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessageService _messageService;
        private IEmailService _emailService;

        public MessagesController(IMessageService messageService, IEmailService emailService)
        {
            _messageService = messageService;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("getMessage/{id}")]
        public IActionResult GetMessage(int id)
        {
            var message = _messageService.GetById(id);
            if (message != null)
            {
                
                return Ok(message);
            }
            return BadRequest(Messages.MessageNotFound);
        }

        [HttpGet]
        [Route("getMessages")]
        public IActionResult GetMessages()
        {
            var getMessages = _messageService.GetAll();
            if (getMessages != null)
            {
                return Ok(getMessages);
            }
            return BadRequest();


        }

        [HttpPost]
        [Route("sendMessage")]
        public IActionResult SendMessage(MessageForCreationDTO model)
        {

            if (ModelState.IsValid)
            {
                var message = new Message
                {

                    MessageHeader = model.MessageHeader,
                    MessageBody = model.MessageBody,
                    Email = model.Email,
                    FullName = model.FullName

                };
                _messageService.Create(message); 
                _emailService.SendMail(model.FullName, model.Email, model.MessageHeader, 
                    $"{model.MessageBody} hakkında attığınız mesaj bize ulaştı.\n\nMesajınızı değerlendirilerek en kısa sürede size geri dönüş yapacaktır.");
                return Ok(Messages.MessageSend);

            }
            return BadRequest(Messages.ModelNullOrEmpty);

        }
        [HttpPost]
        [Route("answerMessage")]
        public IActionResult AnswerMessage(MessageForCreationDTO model)
        {

            if (ModelState.IsValid)
            {
                var message = new Message
                {

                    MessageHeader = model.MessageHeader,
                    MessageBody = model.Answer,
                    Email = model.Email,
                    FullName = model.FullName

                };
                _messageService.Create(message);
                _emailService.SendMail(model.FullName, model.Email, model.MessageHeader, $"{model.Answer} ");
                return Ok(Messages.MessageSend);

            }
            return BadRequest(Messages.ModelNullOrEmpty);

        }


    }
}
