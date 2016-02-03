using System;
using System.Linq;

namespace Complexify_CSharp
{
    public class Complexify
    {
        private static Int16 MIN_COMPLEXITY = 49;
        private static Int16 MAX_COMPLEXITY = 120;

        private static Int32[][] CHARSETS = new Int32[][] {
            // Commonly Used
            ////////////////////
            new Int32[]{0x0020, 0x0020}, // Space
            new Int32[]{0x0030, 0x0039}, // Numbers
            new Int32[]{0x0041, 0x005A}, // Uppercase
            new Int32[]{0x0061, 0x007A}, // Lowercase
            new Int32[]{0x0021, 0x002F}, // Punctuation
            new Int32[]{0x003A, 0x0040}, // Punctuation
            new Int32[]{0x005B, 0x0060}, // Punctuation
            new Int32[]{0x007B, 0x007E}, // Punctuation
            // Everything Else
            ////////////////////
            new Int32[]{0x0080, 0x00FF}, // Latin-1 Supplement
            new Int32[]{0x0100, 0x017F}, // Latin Extended-A
            new Int32[]{0x0180, 0x024F}, // Latin Extended-B
            new Int32[]{0x0250, 0x02AF}, // IPA Extensions
            new Int32[]{0x02B0, 0x02FF}, // Spacing Modifier Letters
            new Int32[]{0x0300, 0x036F}, // Combining Diacritical Marks
            new Int32[]{0x0370, 0x03FF}, // Greek
            new Int32[]{0x0400, 0x04FF}, // Cyrillic
            new Int32[]{0x0530, 0x058F}, // Armenian
            new Int32[]{0x0590, 0x05FF}, // Hebrew
            new Int32[]{0x0600, 0x06FF}, // Arabic
            new Int32[]{0x0700, 0x074F}, // Syriac
            new Int32[]{0x0780, 0x07BF}, // Thaana
            new Int32[]{0x0900, 0x097F}, // Devanagari
            new Int32[]{0x0980, 0x09FF}, // Bengali
            new Int32[]{0x0A00, 0x0A7F}, // Gurmukhi
            new Int32[]{0x0A80, 0x0AFF}, // Gujarati
            new Int32[]{0x0B00, 0x0B7F}, // Oriya
            new Int32[]{0x0B80, 0x0BFF}, // Tamil
            new Int32[]{0x0C00, 0x0C7F}, // Telugu
            new Int32[]{0x0C80, 0x0CFF}, // Kannada
            new Int32[]{0x0D00, 0x0D7F}, // Malayalam
            new Int32[]{0x0D80, 0x0DFF}, // Sinhala
            new Int32[]{0x0E00, 0x0E7F}, // Thai
            new Int32[]{0x0E80, 0x0EFF}, // Lao
            new Int32[]{0x0F00, 0x0FFF}, // Tibetan
            new Int32[]{0x1000, 0x109F}, // Myanmar
            new Int32[]{0x10A0, 0x10FF}, // Georgian
            new Int32[]{0x1100, 0x11FF}, // Hangul Jamo
            new Int32[]{0x1200, 0x137F}, // Ethiopic
            new Int32[]{0x13A0, 0x13FF}, // Cherokee
            new Int32[]{0x1400, 0x167F}, // Unified Canadian Aboriginal Syllabics
            new Int32[]{0x1680, 0x169F}, // Ogham
            new Int32[]{0x16A0, 0x16FF}, // Runic
            new Int32[]{0x1780, 0x17FF}, // Khmer
            new Int32[]{0x1800, 0x18AF}, // Mongolian
            new Int32[]{0x1E00, 0x1EFF}, // Latin Extended Additional
            new Int32[]{0x1F00, 0x1FFF}, // Greek Extended
            new Int32[]{0x2000, 0x206F}, // General Punctuation
            new Int32[]{0x2070, 0x209F}, // Superscripts and Subscripts
            new Int32[]{0x20A0, 0x20CF}, // Currency Symbols
            new Int32[]{0x20D0, 0x20FF}, // Combining Marks for Symbols
            new Int32[]{0x2100, 0x214F}, // Letterlike Symbols
            new Int32[]{0x2150, 0x218F}, // Number Forms
            new Int32[]{0x2190, 0x21FF}, // Arrows
            new Int32[]{0x2200, 0x22FF}, // Mathematical Operators
            new Int32[]{0x2300, 0x23FF}, // Miscellaneous Technical
            new Int32[]{0x2400, 0x243F}, // Control Pictures
            new Int32[]{0x2440, 0x245F}, // Optical Character Recognition
            new Int32[]{0x2460, 0x24FF}, // Enclosed Alphanumerics
            new Int32[]{0x2500, 0x257F}, // Box Drawing
            new Int32[]{0x2580, 0x259F}, // Block Elements
            new Int32[]{0x25A0, 0x25FF}, // Geometric Shapes
            new Int32[]{0x2600, 0x26FF}, // Miscellaneous Symbols
            new Int32[]{0x2700, 0x27BF}, // Dingbats
            new Int32[]{0x2800, 0x28FF}, // Braille Patterns
            new Int32[]{0x2E80, 0x2EFF}, // CJK Radicals Supplement
            new Int32[]{0x2F00, 0x2FDF}, // Kangxi Radicals
            new Int32[]{0x2FF0, 0x2FFF}, // Ideographic Description Characters
            new Int32[]{0x3000, 0x303F}, // CJK Symbols and Punctuation
            new Int32[]{0x3040, 0x309F}, // Hiragana
            new Int32[]{0x30A0, 0x30FF}, // Katakana
            new Int32[]{0x3100, 0x312F}, // Bopomofo
            new Int32[]{0x3130, 0x318F}, // Hangul Compatibility Jamo
            new Int32[]{0x3190, 0x319F}, // Kanbun
            new Int32[]{0x31A0, 0x31BF}, // Bopomofo Extended
            new Int32[]{0x3200, 0x32FF}, // Enclosed CJK Letters and Months
            new Int32[]{0x3300, 0x33FF}, // CJK Compatibility
            new Int32[]{0x3400, 0x4DB5}, // CJK Unified Ideographs Extension A
            new Int32[]{0x4E00, 0x9FFF}, // CJK Unified Ideographs
            new Int32[]{0xA000, 0xA48F}, // Yi Syllables
            new Int32[]{0xA490, 0xA4CF}, // Yi Radicals
            new Int32[]{0xAC00, 0xD7A3}, // Hangul Syllables
            new Int32[]{0xD800, 0xDB7F}, // High Surrogates
            new Int32[]{0xDB80, 0xDBFF}, // High Private Use Surrogates
            new Int32[]{0xDC00, 0xDFFF}, // Low Surrogates
            new Int32[]{0xE000, 0xF8FF}, // Private Use
            new Int32[]{0xF900, 0xFAFF}, // CJK Compatibility Ideographs
            new Int32[]{0xFB00, 0xFB4F}, // Alphabetic Presentation Forms
            new Int32[]{0xFB50, 0xFDFF}, // Arabic Presentation Forms-A
            new Int32[]{0xFE20, 0xFE2F}, // Combining Half Marks
            new Int32[]{0xFE30, 0xFE4F}, // CJK Compatibility Forms
            new Int32[]{0xFE50, 0xFE6F}, // Small Form Variants
            new Int32[]{0xFE70, 0xFEFE}, // Arabic Presentation Forms-B
            new Int32[]{0xFEFF, 0xFEFF}, // Specials
            new Int32[]{0xFF00, 0xFFEF}, // Halfwidth and Fullwidth Forms
            new Int32[]{0xFFF0, 0xFFFD}  // Specials
        };


        /*
            Generated from 500 worst passwords and 401 banned twiiter passwords as of 20150520.
            @source http://www.skullsecurity.org/wiki/index.php/Passwords
            Filtered to remove any passwords shorter than 4 characters, as these will cause
            unwanted behaviour when in strict banning mode.
        */
        private static String[] BANLIST = new String[] {"0","1111","1212","1234","1313","2000","2112","2222",
        "3333","4128","4321","4444","5150","5555","6666","6969","7777","aaaa",
        "alex","asdf","baby","bear","beer","bill","blue","cock","cool","cunt",
        "dave","dick","eric","fire","fish","ford","fred","fuck","girl","golf",
        "jack","jake","john","king","love","mark","matt","mike","mine","pass",
        "paul","porn","rock","sexy","shit","slut","star","test","time","tits",
        "wolf","xxxx","11111","12345","angel","apple","beach","billy","bitch",
        "black","boobs","booty","brian","bubba","buddy","chevy","chris","cream",
        "david","dirty","eagle","enjoy","enter","frank","girls","great","green",
        "happy","hello","horny","house","james","japan","jason","juice","kelly",
        "kevin","kitty","lover","lucky","magic","money","movie","music","naked",
        "ou812","paris","penis","peter","porno","power","pussy","qwert","sammy",
        "scott","smith","stars","steve","super","teens","tiger","video","viper",
        "white","women","xxxxx","young","111111","112233","121212","123123",
        "123456","131313","232323","654321","666666","696969","777777","987654",
        "aaaaaa","abc123","abcdef","access","action","albert","alexis","amanda",
        "andrea","andrew","angela","angels","animal","apollo","apples","arthur",
        "asdfgh","ashley","august","austin","badboy","bailey","banana","barney",
        "batman","beaver","beavis","bigdog","birdie","biteme","blazer","blonde",
        "blowme","bonnie","booboo","booger","boomer","boston","brandy","braves",
        "brazil","bronco","buster","butter","calvin","camaro","canada","carlos",
        "carter","casper","cheese","coffee","compaq","cookie","cooper","cowboy",
        "dakota","dallas","daniel","debbie","dennis","diablo","doctor","doggie",
        "donald","dragon","dreams","driver","eagle1","eagles","edward","erotic",
        "falcon","fender","flower","flyers","freddy","fucked","fucker","fuckme",
        "gators","gemini","george","giants","ginger","golden","golfer","gordon",
        "guitar","gunner","hammer","hannah","harley","helpme","hentai","hockey",
        "horney","hotdog","hunter","iceman","iwantu","jackie","jaguar","jasper",
        "jeremy","johnny","jordan","joseph","joshua","junior","justin","killer",
        "knight","ladies","lakers","lauren","legend","little","london","lovers",
        "maddog","maggie","magnum","marine","martin","marvin","master","matrix",
        "member","merlin","mickey","miller","monica","monkey","morgan","mother",
        "muffin","murphy","nascar","nathan","nicole","nipple","oliver","orange",
        "parker","peanut","pepper","player","please","pookie","prince","purple",
        "qazwsx","qwerty","rabbit","rachel","racing","ranger","redsox","robert",
        "rocket","runner","russia","samson","sandra","saturn","scooby","secret",
        "sexsex","shadow","shaved","sierra","silver","skippy","slayer","smokey",
        "snoopy","soccer","sophie","spanky","sparky","spider","squirt","steven",
        "sticky","stupid","suckit","summer","surfer","sydney","taylor","tennis",
        "teresa","tester","theman","thomas","tigers","tigger","tomcat","topgun",
        "toyota","travis","tucker","turtle","united","vagina","victor","viking",
        "voodoo","walter","willie","wilson","winner","winter","wizard","xavier",
        "xxxxxx","yamaha","yankee","yellow","zxcvbn","zzzzzz","1234567","7777777",
        "8675309","abgrtyu","amateur","anthony","arsenal","asshole","bigcock",
        "bigdick","bigtits","bitches","blondes","blowjob","bond007","brandon",
        "broncos","bulldog","cameron","captain","charles","charlie","chelsea",
        "chester","chicago","chicken","college","cowboys","crystal","cumming",
        "cumshot","diamond","dolphin","extreme","ferrari","fishing","florida",
        "forever","freedom","fucking","fuckyou","gandalf","gateway","gregory",
        "heather","hooters","hunting","jackson","jasmine","jessica","johnson",
        "leather","letmein","madison","matthew","maxwell","melissa","michael",
        "monster","mustang","naughty","ncc1701","newyork","nipples","packers",
        "panther","panties","patrick","peaches","phantom","phoenix","porsche",
        "private","pussies","raiders","rainbow","rangers","rebecca","richard",
        "rosebud","scooter","scorpio","shannon","success","testing","thunder",
        "thx1138","tiffany","trouble","twitter","voyager","warrior","welcome",
        "william","winston","yankees","zxcvbnm","11111111","12345678","access14",
        "baseball","bigdaddy","butthead","cocacola","computer","corvette",
        "danielle","dolphins","einstein","firebird","football","hardcore",
        "iloveyou","internet","jennifer","marlboro","maverick","mercedes",
        "michelle","midnight","mistress","mountain","nicholas","password",
        "princess","qwertyui","redskins","redwings","rush2112","samantha",
        "scorpion","srinivas","startrek","starwars","steelers","sunshine",
        "superman","swimming","trustno1","victoria","whatever","xxxxxxxx",
        "password1","password12","password123"};

        private Int16 _minimumChars;
        private float _strengthScaleFactor;
        private BanMode _banMode;

        public Complexify(Int16 minimumChars = 8, float strengthScaleFactor = 1f, BanMode banMode = BanMode.strict)
        {
            this._minimumChars = minimumChars;
            this._strengthScaleFactor = strengthScaleFactor;
            this._banMode = banMode;
        }

        private Int32 AdditionalComplexityForCharset(String str, Int32[] charset)
        {
            for (var i = str.Length - 1; i >= 0; i--)
            {
                if (charset[0] <= Convert.ToUInt32(str[i]) && Convert.ToUInt32(str[i]) <= charset[1])
                {
                    return charset[1] - charset[0] + 1;
                }
            }
            return 0;
        }

        private bool InBanlist(String str)
        {
            if (this._banMode.Equals(BanMode.strict))
            {
                for (var i = 0; i < BANLIST.Length; i++)
                {
                    if (BANLIST[i].ToLower().Contains(str.ToLower())) return true;
                }
                return false;
            }
            else
            {
                return BANLIST.Contains(str);
            }
        }

        public bool EvaluateSecurity(String password, out Int32 Complexity)
        {
            double complexity = 0;
            bool valid = false;

            //Reset complexity to 0 when banned password is found
            if (!InBanlist(password))
            {
                // Add character complexity
                for (var i = CHARSETS.Length - 1; i >= 0; i--)
                {
                    complexity += AdditionalComplexityForCharset(password, CHARSETS[i]);
                }
            }
            else
            {
                complexity = 1;
            }

            // Use natural log to produce linear scale
            complexity = Math.Log(Math.Pow(complexity, password.Length)) * (1 / this._strengthScaleFactor);

            valid = (complexity > MIN_COMPLEXITY && password.Length >= this._minimumChars);

            // Scale to percentage, so it can be used for a progress bar
            complexity = (complexity / MAX_COMPLEXITY) * 100;
            complexity = (complexity > 100) ? 100 : complexity;

            Complexity = Convert.ToInt32(complexity);

            return valid;
        }
    }
    
    public enum BanMode
    {
        strict,
        loose
    }
}