using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
/*

[ ] Read ',' (which converts number to char)
[ ] Allowed new_line, space or tab in *.bf

*/
public class Program {

 public static void check_tape_dic(Dictionary < int, int > tape_dic, int pos_tape) {
  int pos_tape_value = 0;

  if (tape_dic.ContainsKey(pos_tape) == false) {
   tape_dic.Add(pos_tape, pos_tape_value);
  }
 }
 public static void Main(string[] args) {
  //string raw_commands = @ "+[>[<->+[>+++>[+++++++++++>][]-[<]>-]]++++++++++<]>>>>>>----.<<+++.<-..+++.<-.>>>.<<.+++.------.>-.<<+.<.";
  if (args.Length < 1) throw new ArgumentNullException("no bf file");
  string bf_filepath = args[0];
  string raw_commands = File.ReadAllText(bf_filepath);
  string commands = string.Empty;
  // remove all characters except []<>+-.,
  commands = Regex.Replace(raw_commands, @"[^\[\]<>\+-\.,]", string.Empty);
  int commands_size = commands.Length;
  int pos_command = 0;
  int pos_tape = 0;
  int bracket = 1;
  // key: pos_tape -> value: pos_tape_value
  Dictionary < int, int > tape_dic = new Dictionary < int, int > ();
  while (pos_command < commands_size) {
   check_tape_dic(tape_dic, pos_tape);
   switch (commands[pos_command]) {
    case '[':
     if (tape_dic[pos_tape] == 0) {
      bracket = 1;
      while (bracket >= 1) {
       pos_command++;
       if (commands[pos_command] == '[') bracket++;
       else if (commands[pos_command] == ']') bracket--;
      }
     }
     break;
    case ']':
     if (tape_dic[pos_tape] != 0) {
      bracket = 1;
      while (bracket >= 1) {
       pos_command--;
       if (commands[pos_command] == '[') bracket--;
       else if (commands[pos_command] == ']') bracket++;
      }
     }
     break;
    case '+':
     tape_dic[pos_tape]++;
     break;
    case '-':
     tape_dic[pos_tape]--;
     break;
    case '>':
     pos_tape++;
     break;
    case '<':
     pos_tape--;
     break;
    case '.':
     Console.Write("{0}", (char) tape_dic[pos_tape]);
     break;
    case ',':
     tape_dic[pos_tape] = (int) Console.Read();
     break;
    default:
     break;
   }
   pos_command++;
  }
 }
}
