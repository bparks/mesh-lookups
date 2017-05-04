## A quick vertex calculator

Given a 60px by 60px square divided into 36 10px-by-10px squares, then further
divided by diagonal lines from top left to bottom right such that each of the 36
squares consists of a top-right triangle, then a bottom-left triangle, and
labeled such that each **row** of triangles has one of the letters A through F
and each subsequent triangle from left to right has a number 1 through 12 (that
is, the first triangle is "A1" and the next, "A2", is above and to the right of
it, completing the top-left 10px-by-10px square), this program:

1. computes the vertices, by face, of every triangle in the mesh
2. looks up a face specified by the 3 points that define it

### To build for OS X or linux

1. Install mono
2. Run `make`
3. Run `make run` to run the examples, or run something like the following:

```
mono matrix.exe # Prints vertices
mono matrix.exe 10 10 20 10 20 20 # Looks up a face by the vertices specified
# Format above is x1 y1 x2 y2 x3 y3
```

### To build for Windows

1. Open MeshLookups.csproj in Visual Studio
2. Click "Start"
3. To update command-line arguments, double-click the project Properties node in
   Solution Explorer, then edit "Command Line Arguments" on the "Debug" tab