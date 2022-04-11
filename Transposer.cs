using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NapalmCT
{
    public enum Note : byte
    {
        A,
        ASharp,
        B,
        C,
        CSharp,
        D,
        DSharp,
        E,
        F,
        FSharp,
        G,
        GSharp,
    }
    
    public class Transposer
    {
        public static string[] NoteNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

        public static void Demo()
        {
            Transposer trans = new Transposer();
            string input = "G D Em C Am C Am C";
            string output = trans.Transpose(input);
            Console.WriteLine("INPUT:  " + input);
            Console.WriteLine("OUTPUT: " + output);
        }

        public string Transpose(string input)
        {
            string output = "";
            string[] notes = input.Split(' ');
            for (int i = 0; i < notes.Length; i++)
            {
                bool minor = false;
                string notestr = notes[i];
                if (notestr.Length > 1)
                {
                    if (notestr[1] == 'm') { minor = true; notestr = notestr[0].ToString(); }
                }                

                Transposer trans = new Transposer();
                Note old = trans.GetNoteFromName(notestr);
                Note note = trans.Transpose(old, 9);
                string name = trans.GetNameFromNote(note);
                if (minor) { name += "m"; }
                output += name.PadRight(2, ' ') + " ";
            }
            return output;
        }

        public Note GetNoteFromName(string note)
        {
            for (int i = 0; i < NoteNames.Length; i++) { if (NoteNames[i] == note.ToUpper()) { return (Note)i; } }
            Console.WriteLine("Invalid note '" + note + "'");
            return Note.A;
        }

        public string GetNameFromNote(Note note)
        {
            if (note < (Note)0 || note > Note.GSharp) { Console.WriteLine("Invalid note id " + ((int)note).ToString()); return ""; }
            return NoteNames[(int)note];
        }

        public Note Transpose(Note note, int semitones)
        {
            int index = GetOffset((int)note + semitones);
            return (Note)index;
        }

        private int GetOffset(int semitones)
        {
            int s = semitones % 12;
            return s;
        }
    }
}
