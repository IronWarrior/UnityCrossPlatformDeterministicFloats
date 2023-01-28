This repo contains tests to check the determinism (consistency) of the floating point operations on different Unity build platforms and CPUs. Cross platform determinism has many applications, and is essential for some real-time networking paradigms in video games.

Tests normal numbers, denormal numbers, NaN and infinities, along with an amount of randomly generated values. All tests can be found in `Tests.cs` and `TranscendentalTests.cs`. The current tests cover:

- Arithmetic (+ - * /)
- Comparisons (> < >= <= == !=)
- Casts (to and from `int`/`float`)
- Multiple operand arithmetic
- Triginometry functions found in `System.Math`

All tests here are performed using the IL2CPP backend, with the Windows standalone build results being used as the ground truth. The results here use a random value count of 500 and a seed of 0. If you test on a setup not yet listed here, please feel free to report your results to be added to the table.

### Disclaimer

The author of this repo is not an expert in floating point arithmetic determinism. If you have any improvements to add please feel free to contribute or correct any oversights.

## Results Summary

Between Windows Standalone, Android native, and WebGL (via a browser in Windows, Android, iOS and Mac), the only non-deterministic results reported were in trig functions contained in `System.Math`. There are a couple likely reasons for this.

- `System.Math` works on `doubles`, while this test works with `floats` and does not check if casting between the two is deterministic.
- More likely, this is due to [.NET backing System.Math trig methods (among others) with unmanaged code](https://referencesource.microsoft.com/#mscorlib/system/math.cs) that may contain platform-specific differences.

Because the basic arithmetic operations *are* deterministic, a workaround for the above could be to either reimplement any required functions in managed code, or compile your own native library and ensure that results are consistent.

### Notes

* Operations involving NaN floats return non-deterministic results. This should not be an issue for applications that require determinism, as either
  * NaNs create serious enough bugs that desyncs no longer matter.
  * The developer can choose to check for NaNs after each operation and resolve the issue there (this repo simply treats all NaN results as if they were the same bit sequence).
* Unity's documentation does not make any statement on float determinism either way, so even with consistent results there is no guarantee future versions do not change this.
* [ARMv7 apparently handles denormal numbers differently from ARMv8](https://stackoverflow.com/a/53993942), so should not be a surprise if it desyncs there. 

### Data

| Platform           | Device                                       | Errors                |
|--------------------|----------------------------------------------|-----------------------|
| Windows Standalone | Intel(R) Core(TM) i7-10700K                  | Ground truth          |
| Android            | Google Pixel 4a                              | 57 in Trig functions  |
| WebGL              | Chrome, Windows, Intel(R) Core(TM) i7-10700K | 252 in Trig functions |
| WebGL              | Chrome, Android, Google Pixel 4a             | 252 in Trig functions |
| WebGL              | Safari, iPhone 6s                            | 252 in Trig functions |
| WebGL              | Safari, MacOS, Intel i5                      | 252 in Trig functions |

## Running the tests

* Download [inputs.txt](https://github.com/IronWarrior/UnityCrossPlatformDeterministicFloats/files/10523786/inputs.txt) and [results.txt](https://github.com/IronWarrior/UnityCrossPlatformDeterministicFloats/files/10523787/results.txt). (If you have a Windows PC with an Intel CPU, you can generate the ground truth with a count of 500 and seed of 0. The resulting file will have CRC64=0B6E1BE570134ABC.)
* Copy these files to the `StreamingAssets` directory.
* Build to your target platform to run the test on it.
* Once running, select `Execute Test` and toggle off any tests you do not wish to perform.

### Extending the tests

Tests can be added by creating a new class that implements `ITest`, and then adding it to one of the arrays in `TestRunner` (depending on whether the test takes one or two operands). New hardcoded test inputs can be added in `Generator`. Please name these appropriately, as the names are used when reporting non-deterministic results.
