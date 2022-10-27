using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Dtos
{
    public class UserDto
    {
        public static User FromJson(JObject json)
        {
            var jsonId = json["Id"];
            var id = Guid.NewGuid();
            if(jsonId.ToString()!="")
            {
                id = jsonId.ToObject<Guid>();
            }
            var jsonPerson = json["PersonId"];
            var personId = Guid.Empty;
            if (jsonPerson.ToString()!="")
            {
                personId = jsonPerson.ToObject<Guid>();
            }
            var username = json["Username"].ToString();
            var role = json["RoleId"];
            var roleId = Guid.Empty;
            if (role.ToString() != "")
            {
                roleId = role.ToObject<Guid>();
            }
            var email = json["Email"].ToString();

            var password = json["Password"].ToString();
            return new User(id,username,personId,roleId,email,password);
        }
        public static User FromJsonPerson(JObject json)
        {
            var jsonId = json["Id"];
            var id = Guid.NewGuid();
            if (jsonId.ToString() != "")
            {
                id = jsonId.ToObject<Guid>();
            }
            var username = json["Username"].ToString();
            var email = json["Email"].ToString();
            var password = json["Old-password"].ToString();
            return new User(id, username, Guid.Empty, Guid.Empty, email, password);
        }
    }
}
/*
{
  "Id": null,
  "Username": "test",
  "Password": "test",
  "PersonId": null,
  "RoleId": "",
  "Email": "test@gmail.com"
},
{
  "Id": "325E8761-FC36-43DA-BA04-E87E4D92C93A",
  "Username": "LEGION2",
  "Password": "LEGION2",
  "PersonId": "5C813AC0-E2EC-431A-B9D6-468C56BB2AB4",
  "RoleId": "",
  "Email": "LEGION2@gmail.com"
}
 * */