using System;
using System.Threading.Tasks;

public interface IAdventDay {
    int Day { get; }

    void Solve(string[] input);
}