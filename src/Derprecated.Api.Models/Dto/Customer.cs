// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

﻿namespace Derprecated.Api.Models.Dto
{
    public class Customer : IPrimaryKeyable
    {
      public int Id { get; set; }

      public string Name { get; set; }
      public string Email { get; set; }
      public string PhoneNumber {get; set;}
    }
}
