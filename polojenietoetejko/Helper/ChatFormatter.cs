using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace polojenietoetejko.Helper
{
    internal class Token
    {
        public string? Text { get; set;  }
        public List<FontStyle>? Style { get; set; } = new List<FontStyle>();
        public Color? Color { get; set;  }
    }
    enum MatchType
    {
        Plain,
        Bold,
        Italic,
        Strike,
        Color
    }
    internal static class ChatFormatter
    {
        public static void AppendFormattedText(RichTextBox rcb, string message)
        {
            List<Token> tokens = parseText(message);
            foreach(Token token in tokens)
            {
                FontStyle style = FontStyle.Regular;
                foreach(var tokenStyle in token.Style)
                {
                    style |= tokenStyle;
                }
                rcb.SelectionFont = new Font(rcb.Font, style);
                rcb.SelectionColor = token.Color ?? rcb.ForeColor;
                rcb.AppendText(token.Text);
            }
            rcb.SelectionFont = rcb.Font;
            rcb.SelectionColor = rcb.ForeColor;
            rcb.AppendText("\n");
        }
        private static List<Token> parseText(string message)
        {
            var tokens = new List<Token>();
            Regex regex = new(@"\*(.*?)\*|_(.*?)_|~(.*?)~|\[(\w+)\](.*?)\[/\4\]", RegexOptions.Singleline);

            int lastIndex = 0;
            foreach (Match match in regex.Matches(message))
            {
                if(match.Index > lastIndex)
                {
                    tokens.Add(new Token
                    {
                        Text = message.Substring(lastIndex, match.Index - lastIndex)
                    });
                }
                MatchType type = match.Groups[1].Success ? MatchType.Bold 
                    : match.Groups[2].Success ? MatchType.Italic 
                    : match.Groups[3].Success ? MatchType.Strike 
                    : match.Groups[4].Success ? MatchType.Color 
                    : MatchType.Plain;
                switch (type)
                {
                    case MatchType.Bold:
                        tokens.Add(new Token
                        {
                            Text = match.Groups[1].Value,
                            Style = new List<FontStyle>
                            { FontStyle.Bold
                            }
                        });
                        break;
                    case MatchType.Italic:
                        tokens.Add(new Token
                        {
                            Text = match.Groups[2].Value,
                            Style = new List<FontStyle>
                            { FontStyle.Italic
                            }
                        });
                        break;
                    case MatchType.Strike:
                        tokens.Add(new Token
                        {
                            Text = match.Groups[3].Value,
                            Style = new List<FontStyle>
                            { FontStyle.Bold
                            }
                        });
                        break;
                    case MatchType.Color:
                        Color c = Color.FromName(match.Groups[4].Value);
                        tokens.Add(new Token
                        {
                            Text = match.Groups[5].Value,
                            Color = c.IsEmpty ? null : c
                        });
                        break;
                    case MatchType.Plain:
                        tokens.Add(new Token
                        {
                            Text = match.Value
                        });
                        break;
                    default:
                        break;
                }
                lastIndex = match.Index + match.Length;
            }
            if(lastIndex < message.Length)
            {
                tokens.Add(new Token
                {
                    Text = message.Substring(lastIndex)
                });
            }
            return tokens;
        }
        
    }
    
}
