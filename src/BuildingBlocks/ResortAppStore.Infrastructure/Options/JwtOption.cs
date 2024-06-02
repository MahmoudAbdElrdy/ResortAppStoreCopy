﻿namespace Common.Options
{
  public class JwtOption {
    public string Key { get; set; }

    public string Issuer { get; set; }
    public string Audience { get; set; } 
    public string TokenLifetime { get; set; }
    public int ExpireDays { get; set; }

  }
}