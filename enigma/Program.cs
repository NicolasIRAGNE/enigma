using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enigma
{
    class Program
    {
        class Rotor
        {
            public string wiring;
            public int spot;
            public int offset;
            public int notch;

            public Rotor(string wiring, int spot, int offset, int notch)
            {
                this.wiring = wiring;
                this.spot = spot;
                this.offset = offset;
                this.notch = notch;
            }
            public void Increment()
            {
                this.offset++;
                if (offset > 26)
                    offset -= 26;
            }
        }

        public static class Ref
        {
            public const string I = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
            public const string II = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
            public const string III = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
            public const string IV = "ESOVPZJAYQUIRHXLNFTGKDCMWB";
            public const string V = "VZBRGITYUPSDNHLXAWMJQOFECK";
            public const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            public const string steckerbrett = "ZBCDEFGHIJKLMNOPQRSTUVWXYA";
            public const string ReflectorA = "EJMZALYXVBWFCRQUONTSPIKHGD";
            public const string ReflectorB = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            public const string ReflectorC = "FVPJIAOYEDRZXWGCTKUQSBNMHL";
        }
        class Reflector
        {
            public string wiring;

            public Reflector(string wiring)
            {
                this.wiring = wiring;
            }
        }

        static int shift(int letter, Rotor[] rotors, int pos, bool flag)
        {
            int index;
            if (!flag)
            {
                if (pos == 0)
                    rotors[pos].Increment();
                if (pos != 0 && rotors[pos - 1].offset == rotors[pos - 1].notch)
                    rotors[pos].Increment();
            }
            letter = (letter + rotors[pos].offset) % 26;
            if (!flag)
                index = Convert.ToInt32(rotors[pos].wiring[letter] - 65);
            else
                index = Convert.ToInt32(Ref.alphabet[rotors[pos].wiring.IndexOf(Convert.ToChar(letter + 65))] - 65);
            index = index - rotors[pos].offset;
            if (index < 0)
                index += 26;
            return (index);
        }

        static int shiftReflector(int letter, Reflector reflector)
        {
            int index = Ref.alphabet.IndexOf(reflector.wiring[Ref.alphabet.IndexOf((Convert.ToChar(letter + 65)))]);
            return (index);
        }

        static string shift_message(string message, Rotor[] rotors, Reflector reflector, Reflector steckerbrett)
        {
            int i = 0;
            int j = message.Length;
            int ltr;
            string ret = "";
            while (i < j)
            {
                ltr = message[i] - 65;
                ltr = shiftReflector(ltr, steckerbrett);
                ltr = shift(ltr, rotors, 0, false);
                ltr = shift(ltr, rotors, 1, false);
                ltr = shift(ltr, rotors, 2, false);
                ltr = shiftReflector(ltr, reflector);
                ltr = shift(ltr, rotors, 2, true);
                ltr = shift(ltr, rotors, 1, true);
                ltr = shift(ltr, rotors, 0, true);
                ltr = shiftReflector(ltr, steckerbrett);
                ret += Convert.ToChar(ltr + 65);
                i++;
            }
            return ret;
        }

        static void Main(string[] args)
        {

            string input;
            Rotor[] rotors = new Rotor[3];
            Reflector reflector = new Reflector(Ref.ReflectorB);
            Reflector steckerbrett = new Reflector(Ref.steckerbrett);
            rotors[2] = new Rotor(Ref.I, 1, 0, 17);
            rotors[1] = new Rotor(Ref.II, 2, 0, 5);
            rotors[0] = new Rotor(Ref.III, 3, 0, 22);
            input = Console.ReadLine();
            Console.WriteLine(shift_message(input, rotors, reflector, steckerbrett));
            Console.ReadLine();
        }
    }
}
