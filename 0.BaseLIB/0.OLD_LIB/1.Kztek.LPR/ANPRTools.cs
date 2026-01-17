using System;
using System.Text;
namespace Kztek.LPR
{
	public static class ANPRTools
	{
        public static bool IsMotorPlateNumber(string plateNumber)
        {
            bool ret = false;
            if(plateNumber.Length >= 10 && plateNumber.IndexOf("-") == 2 && (( plateNumber.LastIndexOf("-") == 5 || ((plateNumber.Contains("AA") || plateNumber.Contains("MD")) && plateNumber.LastIndexOf("-") == 6))))
            {
                ret = true;
            }

            return ret;
        }

		public static bool IsValidCarPlateNumber(string strplate)
        {
			if (strplate != "")
			{
				int checkValue = 0;
				for (int index = 0; index < strplate.Length; index++)
				{
					if (Char.IsLetter(strplate[index])) checkValue += 1;
				}
				if (checkValue >0 && checkValue < 3)
				{
					return true;
				}
				else
                {
					if(strplate.IndexOf(' ') == strplate.LastIndexOf(' ') && strplate.Length <= 8)
                    {
						return true;
                    }						
                }					
			}
			return false;
		}

		public static string FixMotorPlateNumber(string plateNumber)
		{
			if (plateNumber.Contains("."))
			{
				plateNumber = plateNumber.Replace(".", "");
			}
			string s = plateNumber.Replace(" ", "");
			if (s.Contains("1I") || s.Contains("I1"))
				plateNumber = plateNumber.Replace("I", "");
			if (s.Contains("1J") || s.Contains("J1"))
				plateNumber = plateNumber.Replace("J", "");
			plateNumber = plateNumber.Replace("J", "3");

			plateNumber = plateNumber.Replace("O", "0");
			plateNumber = plateNumber.Replace("W", "M");
			plateNumber = plateNumber.Replace("I", "1");
			if (!string.IsNullOrEmpty(plateNumber) && plateNumber.Length >= 10 && plateNumber.IndexOf("-") == 2 && plateNumber.LastIndexOf("-") == 5)
			{
				if (plateNumber[0] == '0')
				{
					plateNumber = "6" + plateNumber.Substring(1);
				}
				else
				{
					if (plateNumber.StartsWith("10"))
					{
						plateNumber = "18" + plateNumber.Substring(2);
					}
				}
				if (!plateNumber.Contains("MD") && !plateNumber.Contains("LD") && !plateNumber.Contains("KT") && !plateNumber.Contains("LA") && !plateNumber.Contains("NG") && !plateNumber.Contains("NN") && !plateNumber.Contains("QT") && !plateNumber.Contains("AE"))
				{
					if (plateNumber.Length == 12 && plateNumber[9] == '-')
					{
						plateNumber = plateNumber.Remove(9, 1);
					}
					else
					{
						if (plateNumber.Length == 10 && plateNumber[7] == '-')
						{
							plateNumber = plateNumber.Remove(7, 1);
						}
					}
					if (plateNumber.Contains("L0"))
					{
						plateNumber = plateNumber.Replace("L0", "LD");
					}
					else
					{
						if (plateNumber.Contains("H0"))
						{
							plateNumber = plateNumber.Replace("H0", "HD");
						}
						else
						{
							if (plateNumber.Contains("F0"))
							{
								plateNumber = plateNumber.Replace("F0", "FD");
							}
							else
							{
								if (plateNumber[4] != 'L' && !(plateNumber[4].ToString() == "Y"))
								{
									if (plateNumber[4] != 'T')
									{
										if (plateNumber[4] == 'Z')
										{
											plateNumber = plateNumber.Remove(4, 1).Insert(4, "2");
											return plateNumber;
										}
										if (!(plateNumber[4].ToString() == "K"))
										{
											if (plateNumber[4] != 'U')
											{
												if (plateNumber[4].ToString() == "G" || plateNumber[4].ToString() == "R")
												{
													plateNumber = plateNumber.Remove(4, 1).Insert(4, "6");
													return plateNumber;
												}
												if (plateNumber[4] == 'S')
												{
													plateNumber = plateNumber.Remove(4, 1).Insert(4, "5");
													return plateNumber;
												}
												if (plateNumber[4] == 'P')
												{
													plateNumber = plateNumber.Remove(4, 1).Insert(4, "7");
													return plateNumber;
												}
												if (plateNumber[4].ToString() == "0" || plateNumber[4].ToString() == "M")
												{
													plateNumber = plateNumber.Remove(4, 1).Insert(4, "8");
													return plateNumber;
												}
												if (char.IsLetter(plateNumber[4]))
												{
													int num = plateNumber.LastIndexOf("-");
													plateNumber = plateNumber.Substring(num + 1);
													return plateNumber;
												}
												return plateNumber;
											}
										}
										plateNumber = plateNumber.Remove(4, 1).Insert(4, "4");
										return plateNumber;
									}
								}
								plateNumber = plateNumber.Remove(4, 1).Insert(4, "1");
							}
						}
					}
				}
			}
			else if (!plateNumber.Contains("-"))
			{
				plateNumber = plateNumber.Replace(" ", "");
				if (plateNumber.Length > 9) plateNumber = plateNumber.Substring(0, 9);
				if (plateNumber.Length >= 5)
				{
					if (plateNumber[1] >= '0' && plateNumber[1] <= '9' && plateNumber[2] >= 'A' && plateNumber[2] <= 'Z' && plateNumber[3] >= '0' && plateNumber[3] <= '9')
					{
						for (int i = 4; i < plateNumber.Length; i++)
						{
							if (plateNumber[i] == 'Z') plateNumber = plateNumber.Substring(0, i) + "2" + plateNumber.Substring(i + 1);
							if (plateNumber[i] == 'O') plateNumber = plateNumber.Substring(0, i) + "0" + plateNumber.Substring(i + 1);
						}
						plateNumber = plateNumber.Substring(0, 2) + "-" + plateNumber.Substring(2, 2) + "-" + plateNumber.Substring(4);
					}
				}
			}
			return plateNumber;
		}
		public static string FixCarPlateNumber(string plateNumber)
		{
			if (!string.IsNullOrEmpty(plateNumber) && plateNumber.Length >= 8)
			{
				if (plateNumber.Contains("."))
				{
					plateNumber = plateNumber.Replace(".", "");
				}
				plateNumber = plateNumber.Replace("O", "0");
				plateNumber = plateNumber.Replace("W", "M");
				plateNumber = plateNumber.Replace("I", "1");
				plateNumber = plateNumber.Replace("-FF", "H");
				if (plateNumber[0] == '0')
				{
					plateNumber = "6" + plateNumber.Substring(1);
				}
				else
				{
					if (plateNumber.StartsWith("10"))
					{
						plateNumber = "18" + plateNumber.Substring(2);
					}
				}
				if (!plateNumber.Contains("NG") && !plateNumber.Contains("NN") && !plateNumber.Contains("QT"))
				{
					if (plateNumber.Length == 12 && plateNumber[9] == '-')
					{
						plateNumber = plateNumber.Remove(9, 1);
					}
					else
					{
						if (plateNumber.Length == 10 && plateNumber[7] == '-')
						{
							plateNumber = plateNumber.Remove(7, 1);
						}
					}
					if (plateNumber.Contains("L0"))
					{
						plateNumber = plateNumber.Replace("L0", "LD");
					}
					else
					{
						if (plateNumber.Contains("C0"))
						{
							plateNumber = plateNumber.Replace("C0", "LD");
						}
						else
						{
							if (plateNumber.Contains("H0"))
							{
								plateNumber = plateNumber.Replace("H0", "LD");
							}
							else
							{
								if (plateNumber.Contains("CD"))
								{
									plateNumber = plateNumber.Replace("CD", "LD");
								}
								else
								{
									if (plateNumber.Contains("ED"))
									{
										plateNumber = plateNumber.Replace("ED", "LD");
									}
									else
									{
										if (plateNumber.Contains("-DLD-"))
										{
											if (plateNumber.Length == 13 && plateNumber.IndexOf('-') == 2 && plateNumber.LastIndexOf('-') == 10)
											{
												plateNumber = plateNumber.Substring(1).Replace("-", "");
												plateNumber = plateNumber.Substring(0, 1) + "0-LD-" + plateNumber.Substring(4);
											}
										}
										else
										{
											if (plateNumber.Length >= 10 && plateNumber.IndexOf("-") == 2 && plateNumber.LastIndexOf("-") == 5 && !plateNumber.Contains("MD") && !plateNumber.Contains("LD") && !plateNumber.Contains("KT") && !plateNumber.Contains("LA"))
											{
												if (plateNumber[4] != 'L' && !(plateNumber[4].ToString() == "Y"))
												{
													if (plateNumber[4] != 'T')
													{
														if (plateNumber[4] == 'Z')
														{
															plateNumber = plateNumber.Remove(4, 1).Insert(4, "2");
															return plateNumber;
														}
														if (!(plateNumber[4].ToString() == "K"))
														{
															if (plateNumber[4] != 'U')
															{
																if (plateNumber[4].ToString() == "G" || plateNumber[4].ToString() == "R")
																{
																	plateNumber = plateNumber.Remove(4, 1).Insert(4, "6");
																	return plateNumber;
																}
																if (plateNumber[4] == 'S')
																{
																	plateNumber = plateNumber.Remove(4, 1).Insert(4, "5");
																	return plateNumber;
																}
																if (plateNumber[4] == 'P')
																{
																	plateNumber = plateNumber.Remove(4, 1).Insert(4, "7");
																	return plateNumber;
																}
																if (plateNumber[4].ToString() == "0" || plateNumber[4].ToString() == "M")
																{
																	plateNumber = plateNumber.Remove(4, 1).Insert(4, "8");
																	return plateNumber;
																}
																return plateNumber;
															}
														}
														plateNumber = plateNumber.Remove(4, 1).Insert(4, "4");
														return plateNumber;
													}
												}
												plateNumber = plateNumber.Remove(4, 1).Insert(4, "1");
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return plateNumber;
		}
		public static byte[] GetKey(string lprEngineProductKey)
		{
			byte[] result = null;
            CryptorEngineLPR.key = "@#$NXT SOFTWARE$#@";

            if (lprEngineProductKey != null && lprEngineProductKey != "")
			{
				UTF8Encoding uTF8Encoding = new UTF8Encoding();
				if (lprEngineProductKey.StartsWith("<?xml") && lprEngineProductKey.EndsWith("</license>"))
				{
					result = uTF8Encoding.GetBytes(lprEngineProductKey);
				}
				else
				{
					string text = CryptorEngineLPR.Decrypt(lprEngineProductKey, true);
					if (text.StartsWith("<?xml") && text.EndsWith("</license>"))
					{
						result = uTF8Encoding.GetBytes(text);
					}
					else
					{
						CryptorEngineLPR.SetKey("GTS 2021");
						text = CryptorEngineLPR.Decrypt(lprEngineProductKey, true);
						if (text.StartsWith("<?xml") && text.EndsWith("</license>"))
						{
							result = uTF8Encoding.GetBytes(text);
						}
					}
				}
			}
			return result;
		}
	}
}
