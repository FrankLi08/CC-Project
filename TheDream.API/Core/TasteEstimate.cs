using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheDream.API.Models;
using TheDream.DAL.Model;
using TheDream.Model.Models;

namespace TheDream.API.Core
{
    public class TasteEstimate
    {
        public EstimateResult EstimateScore(TasteValue model,List<VegetableDosing> VegModel)
        {
            EstimateResult Result = new EstimateResult();
            List<string> DescriptionList = new List<string>();
            List<string> VegCatagories = new List<string>();
            foreach(var item in VegModel)
            {
                VegCatagories.Add(item.VegValue.Name);
            }
            var response = new SQL().GetVegetableFlavour(VegCatagories);
            int number = response.Where(x => x.FlavourName != null || x.FlavourName != "").Count();
             
            int saltyScore, sweetScore, sourScore, spicyScore, bitterScore, oilScore,FlavourScore;
            DescriptionList.Add(FlavourEstimate(number, out FlavourScore));
            DescriptionList.Add(SaltyEstimate(model.Salty, out saltyScore));
            DescriptionList.Add(SweetEstimate(model.Sweet, out sweetScore));
            DescriptionList.Add(SourEstimate(model.Sour, out sourScore));
            DescriptionList.Add(SpicyEstimate(model.Spicy, out spicyScore));
            DescriptionList.Add(BitterEstimate(model.Bitter, out bitterScore));
            DescriptionList.Add(OilEstimate(model.Oil, out oilScore));
            Result.Description = string.Join(", ", DescriptionList.ToArray());
            Result.Score = 95 - saltyScore - sweetScore - sourScore - spicyScore - bitterScore - oilScore;
            return Result;

        }
        public string SaltyEstimate(double? value, out int saltyScore)
        {
            if (value == null)
            {
                saltyScore = 1;
                return "";
            }
            else
            {

                var DecriptionResult = new Dictionary<Func<int, bool>, string>
            {
             { x => x < 10 ,    "10" },
             { x => x < 100 ,    "1000" },
             { x => x < 1000 ,    "110000"   },
             { x => x < 10000 ,   "22"  } ,
             { x => x < 100000 ,  "212223"  },
             { x => x < 1000000 , "2124443"  }
            };


                //estimate result score.
                var ScoreResult = new Dictionary<Func<int, bool>, int>
            {
             { x => x < 10 ,    1 },
             { x => x < 100 ,   33 },
             { x => x < 1000 ,    4  },
             { x => x < 10000 ,   5 } ,
             { x => x < 100000 ,  6 },
             { x => x < 1000000 ,6 }
            };

                saltyScore = ScoreResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
                return DecriptionResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
            }

        }
        public string SweetEstimate(double? value, out int sweetScore)
        {
            if (value == null)
            {
                sweetScore = 1;
                return "";
            }
            else
            {

                var responseResult = new Dictionary<Func<int, bool>, string>
            {
             { x => x < 10 ,    "10" },
             { x => x < 100 ,    "1000" },
             { x => x < 1000 ,    "110000"   },
             { x => x < 10000 ,   "22"  } ,
             { x => x < 100000 ,  "212223"  },
             { x => x < 1000000 , "2124443"  }
            };
                var ScoreResult = new Dictionary<Func<int, bool>, int>
            {
             { x => x < 10 ,    1 },
             { x => x < 100 ,   33 },
             { x => x < 1000 ,    4  },
             { x => x < 10000 ,   5 } ,
             { x => x < 100000 ,  6 },
             { x => x < 1000000 ,6 }
            };

                sweetScore = ScoreResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
                return responseResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
            }

        }
        public string SourEstimate(double? value, out int sourScore)
        {
            if (value == null)
            {
                sourScore = 1;
                return "";
            }
            else
            {

                var responseResult = new Dictionary<Func<int, bool>, string>
            {
             { x => x < 10 ,    "10" },
             { x => x < 100 ,    "1000" },
             { x => x < 1000 ,    "110000"   },
             { x => x < 10000 ,   "22"  } ,
             { x => x < 100000 ,  "212223"  },
             { x => x < 1000000 , "2124443"  }
            };
                var ScoreResult = new Dictionary<Func<int, bool>, int>
            {
             { x => x < 10 ,    1 },
             { x => x < 100 ,   33 },
             { x => x < 1000 ,    4  },
             { x => x < 10000 ,   5 } ,
             { x => x < 100000 ,  6 },
             { x => x < 1000000 ,6 }
            };
                sourScore = ScoreResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
                return responseResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
            }

        }
        public string SpicyEstimate(double? value, out int spicyScore)
        {
            if (value == null)
            {
                spicyScore = 1;
                return "";
            }
            else
            {

                var responseResult = new Dictionary<Func<int, bool>, string>
            {
             { x => x < 10 ,    "10" },
             { x => x < 100 ,    "1000" },
             { x => x < 1000 ,    "110000"   },
             { x => x < 10000 ,   "22"  } ,
             { x => x < 100000 ,  "212223"  },
             { x => x < 1000000 , "2124443"  }
            };
                var ScoreResult = new Dictionary<Func<int, bool>, int>
            {
             { x => x < 10 ,    1 },
             { x => x < 100 ,   33 },
             { x => x < 1000 ,    4  },
             { x => x < 10000 ,   5 } ,
             { x => x < 100000 ,  6 },
             { x => x < 1000000 ,6 }
            };

                spicyScore = ScoreResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
                return responseResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
            }

        }
        public string BitterEstimate(double? value, out int bitterScore)
        {
            if (value == null)
            {
                bitterScore = 1;
                return "";
            }
            else
            {

                var responseResult = new Dictionary<Func<int, bool>, string>
            {
             { x => x < 10 ,    "10" },
             { x => x < 100 ,    "1000" },
             { x => x < 1000 ,    "110000"   },
             { x => x < 10000 ,   "22"  } ,
             { x => x < 100000 ,  "212223"  },
             { x => x < 1000000 , "2124443"  }
            };
                var ScoreResult = new Dictionary<Func<int, bool>, int>
            {
             { x => x < 10 ,    1 },
             { x => x < 100 ,   33 },
             { x => x < 1000 ,    4  },
             { x => x < 10000 ,   5 } ,
             { x => x < 100000 ,  6 },
             { x => x < 1000000 ,6 }
            };

                bitterScore = ScoreResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
                return responseResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
            }

        }
        public string OilEstimate(double? value, out int oilScore)
        {
            if (value == null)
            {
                oilScore = 1;
                return "";
            }
            else
            {

                var responseResult = new Dictionary<Func<int, bool>, string>
            {
             { x => x < 10 ,    "10" },
             { x => x < 100 ,    "1000" },
             { x => x < 1000 ,    "110000"   },
             { x => x < 10000 ,   "22"  } ,
             { x => x < 100000 ,  "212223"  },
             { x => x < 1000000 , "2124443"  }
            };

                var ScoreResult = new Dictionary<Func<int, bool>, int>
            {
             { x => x < 10 ,    1 },
             { x => x < 100 ,   33 },
             { x => x < 1000 ,    4  },
             { x => x < 10000 ,   5 } ,
             { x => x < 100000 ,  6 },
             { x => x < 1000000 ,6 }
            };

                oilScore = ScoreResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
                return responseResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
            }

        }
        public string FlavourEstimate(int value, out int FlavourScore)
        {
          
        
                var responseResult = new Dictionary<Func<int, bool>, string>
            {
             { x => x < 10 ,    "10" },
             { x => x < 100 ,    "1000" },
             { x => x < 1000 ,    "110000"   },
             { x => x < 10000 ,   "22"  } ,
             { x => x < 100000 ,  "212223"  },
             { x => x < 1000000 , "2124443"  }
            };

                var ScoreResult = new Dictionary<Func<int, bool>, int>
            {
             { x => x < 10 ,    1 },
             { x => x < 100 ,   33 },
             { x => x < 1000 ,    4  },
             { x => x < 10000 ,   5 } ,
             { x => x < 100000 ,  6 },
             { x => x < 1000000 ,6 }
            };

            FlavourScore = ScoreResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
                return responseResult.First(x => x.Key(Convert.ToInt32(value * 100))).Value;
            

        }
    }
}