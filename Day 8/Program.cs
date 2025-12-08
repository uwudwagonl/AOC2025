var lines = File.ReadAllLines("C:\\Users\\benja\\OneDrive\\Desktop\\AOC\\AOC2025\\Day 8\\input.txt");

List<(int x, int y, int z)> junctions = new List<(int, int, int)>();
foreach (string line in lines)
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    string[] parts = line.Split(',');
    int x = int.Parse(parts[0]);
    int y = int.Parse(parts[1]);
    int z = int.Parse(parts[2]);
    junctions.Add((x, y, z));
}

int n = junctions.Count;

List<(double distance, int i, int j)> edges = new List<(double, int, int)>();
for (int i = 0; i < n; i++)
{
    for (int j = i + 1; j < n; j++)
    {
        double dist = Distance(junctions[i], junctions[j]);
        edges.Add((dist, i, j));
    }
}
edges.Sort((a, b) => a.distance.CompareTo(b.distance));

UnionFind uf1 = new UnionFind(n);
int connectionAttempts = 0;

foreach (var (distance, i, j) in edges)
{
    if (connectionAttempts >= 1000) break;
    connectionAttempts++;
    uf1.Union(i, j);
}

Dictionary<int, int> cs = new Dictionary<int, int>();
for (int i = 0; i < n; i++)
{
    int root = uf1.Find(i);
    if (!cs.ContainsKey(root)) cs[root] = 0;
    cs[root]++;
}

List<int> sizes = cs.Values.OrderByDescending(x => x).ToList();
//long r1 = (long)sizes[0] * sizes[1] * sizes[2];

UnionFind uf2 = new UnionFind(n);
int xI = -1, lastJ = -1;

foreach (var (distance, i, j) in edges)
{
    if (uf2.Union(i, j))
    {
        xI = i;
        lastJ = j;
        if (uf2.Count() == 1) break;
    }
}


long result = (long)junctions[xI].x * junctions[lastJ].x;
Console.WriteLine(result);

static double Distance((int x, int y, int z) a, (int x, int y, int z) b)
{
    long dx = a.x - b.x;
    long dy = a.y - b.y;
    long dz = a.z - b.z;
    return Math.Sqrt(dx * dx + dy * dy + dz * dz);
}

class UnionFind
{
    private int[] parent;
    private int[] rank;
    private int circuits;

    public UnionFind(int n)
    {
        parent = new int[n];
        rank = new int[n];
        circuits = n;
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]); 
        }
        return parent[x];
    }

    public bool Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);
        if (rootX == rootY) return false; 
        if (rank[rootX] < rank[rootY]) parent[rootX] = rootY;
        else if (rank[rootX] > rank[rootY]) parent[rootY] = rootX;
        else
        {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
        circuits--;
        return true;
    }

    public int Count() => circuits;
    
}