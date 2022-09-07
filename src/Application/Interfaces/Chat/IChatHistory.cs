using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Interfaces.Chat;

public interface IChatHistory<TUser> where TUser : IChatUser
{
    public long Id { get; set; }
    public string FromUserId { get; set; }
    public string ToUserId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public TUser FromUser { get; set; }
    public TUser ToUser { get; set; }
}
