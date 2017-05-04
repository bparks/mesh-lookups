.PHONY: all cscheck run

all : matrix.exe

run : matrix.exe
	@mono matrix.exe
	@echo "An invalid face:"
	@mono matrix.exe 0 0 60 60 40 40
	@echo "A valid face:"
	@mono matrix.exe 30 40 40 40 40 50

matrix.exe : cscheck Matrix.cs Structures.cs
	mcs -out:matrix.exe Matrix.cs Structures.cs

cscheck :
	@which mcs || echo "No C# compiler available!"
	@which mcs || exit 1
