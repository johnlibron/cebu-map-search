using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CebuMap
{
    public partial class FormMap : Form
    {
        private Dictionary<int, Vertex> vertexList;
        private int vertexCount;
        private int[,] adjacencyMatrix;
        private Vertex startVertex;
        private Vertex targetVertex;
        private Label[] vertexLabel;
        private List<Vertex> pathList;

        public FormMap()
        {
            InitializeComponent();
            vertexList = new Dictionary<int, Vertex>();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            bool isSearch = false;

            // Breadth-First Search
            if (cboSearch.SelectedIndex == 0)
            {
                BreadthFirstSearch(startVertex, targetVertex);
                isSearch = true;
            }
            // Depthth-First Search
            else if (cboSearch.SelectedIndex == 1)
            {
                DepthFirstSearch(startVertex, targetVertex);
                isSearch = true;
            }
            // Greedy Best-First Search
            else if (cboSearch.SelectedIndex == 2)
            {
                GreedyBestFirstSearch(startVertex, targetVertex);
                isSearch = true;
            }
            // A* Search
            else if (cboSearch.SelectedIndex == 3)
            {
                AStarSearch(startVertex, targetVertex);
                isSearch = true;
            }

            if (isSearch)
            {
                cboSearch.Enabled = false;
                btnSearch.Enabled = false;
                btnRemove.Enabled = true;
            }
        }

        private void BtnDisplay_Click(object sender, EventArgs e)
        {
            AddVertices();
            AddEdges();

            //DisplayEdges();
            DisplayVertices();
            
            btnDisplay.Enabled = false;
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            vertexLabel[startVertex.Index].ForeColor = Color.Red;
            vertexLabel[startVertex.Index].BackColor = Color.Red;
            vertexLabel[targetVertex.Index].ForeColor = Color.Red;
            vertexLabel[targetVertex.Index].BackColor = Color.Red;

            lblStartVertex.Text = "Start Vertex:";
            lblTargetVertex.Text = "Target Vertex:";

            startVertex = null;
            targetVertex = null;
            cboSearch.Enabled = false;
            btnSearch.Enabled = false;
            btnRemove.Enabled = false;

            panelGraph.Refresh();
            panelGraph.Invalidate();
        }

        private void AddVertices()
        {
            StreamReader sr = new StreamReader(Environment.CurrentDirectory + "//vertices.txt");
            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                AddVertex(temp[2], Int32.Parse(temp[0]), Int32.Parse(temp[1]));
            }
            sr.Close();
        }

        private void AddEdges()
        {
            vertexLabel = new Label[vertexCount];
            adjacencyMatrix = new int[vertexCount, vertexCount];

            StreamReader sr = new StreamReader(Environment.CurrentDirectory + "//edges.txt");
            string line = null; 
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                int v1 = GetVertexIndex(temp[0]);
                int v2 = GetVertexIndex(temp[1]);
                AddEdge(vertexList[v1], vertexList[v2]);
            }
            sr.Close();
        }

        public void AddVertex(string name, int x, int y)
        {
            vertexList.Add(vertexCount, new Vertex(name, vertexCount, x, y));
            vertexCount++;
        }

        public void AddEdge(Vertex v1, Vertex v2)
        {
            if (v1.Index == v2.Index)
            {
                Console.WriteLine("Not a valid edge");
            }
            else
            {
                if (adjacencyMatrix[v1.Index, v2.Index] != 0)
                {
                    Console.WriteLine("Edge is already present.");
                }
                else
                {
                    adjacencyMatrix[v1.Index, v2.Index] = GetDistance(v1, v2);
                    adjacencyMatrix[v2.Index, v1.Index] = adjacencyMatrix[v1.Index, v2.Index];
                }
            }
        }

        public void DisplayEdges()
        {
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        Point point1 = new Point(vertexList[i].X + 4, vertexList[i].Y + 4);
                        Point point2 = new Point(vertexList[j].X + 4, vertexList[j].Y + 4);
                        Pen pen = new Pen(Color.LightGray, 1);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();
                    }
                }
            }
        }

        public void DisplayVertices()
        {
            foreach (KeyValuePair<int, Vertex> entry in vertexList)
            {
                vertexLabel[entry.Key] = new Label
                {
                    Width = 7,
                    Height = 7,
                    AutoSize = false,
                    Location = new Point(entry.Value.X, entry.Value.Y),
                    BorderStyle = BorderStyle.FixedSingle,
                    ForeColor = Color.Red,
                    BackColor = Color.Red,
                    Text = entry.Value.Index.ToString()
                };
                vertexLabel[entry.Key].Click += new EventHandler(Vertex_Click);
                panelGraph.Controls.Add(vertexLabel[entry.Key]);
            }
        }

        public void Vertex_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            int vertexIndex = Int32.Parse(label.Text);

            if (null == startVertex)
            {
                cboSearch.Enabled = false;
                btnSearch.Enabled = false;
                btnRemove.Enabled = false;
                label.ForeColor = Color.Green;
                label.BackColor = Color.Green;
                startVertex = vertexList[vertexIndex];
                lblStartVertex.Text = "Start Vertex: " + startVertex.X + ", " + startVertex.Y;
            }
            else if (null == targetVertex)
            {
                cboSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnRemove.Enabled = true;
                label.ForeColor = Color.Green;
                label.BackColor = Color.Green;
                targetVertex = vertexList[vertexIndex];
                lblTargetVertex.Text = "Target Vertex: " + targetVertex.X + ", " + targetVertex.Y;
            }
            else
            {
                cboSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnRemove.Enabled = true;
            }
        }

        public int GetVertexIndex(String vertexName)
        {
            foreach (KeyValuePair<int, Vertex> entry in vertexList)
            {
                if (vertexName.Equals(entry.Value.Name))
                {
                    return entry.Key;
                }
            }
            return -1;
        }

        public int GetDistance(Vertex v1, Vertex v2)
        {
            double x = Math.Pow(v2.X - v1.X, 2);
            double y = Math.Pow(v2.Y - v1.Y, 2);
            return (int) Math.Round(Math.Pow((x + y), 0.5));
        }

        
        public void BreadthFirstSearch(Vertex startVertex, Vertex targetVertex)
        {
            bool[] visited = new bool[vertexCount];

            Queue<Vertex> queue = new Queue<Vertex>();
            List<string> pathList = new List<string>();
            
            visited[startVertex.Index] = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                Vertex vertex = queue.Dequeue();
                pathList.Add(vertex.Name);

                if (vertex.Name.Equals(targetVertex.Name))
                {
                    RetracePath(startVertex, targetVertex);
                    break;
                }

                List<Vertex> neighbors = GetNeighbors(vertex).OrderBy(e => e.Distance).ThenBy(e => e.Name).ToList();

                foreach (Vertex neighbor in neighbors)
                {
                    if (!visited[neighbor.Index])
                    {
                        Point point1 = new Point(vertex.X + 3, vertex.Y + 3);
                        Point point2 = new Point(neighbor.X + 3, neighbor.Y + 3);
                        Pen pen = new Pen(Color.Blue, 1);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();

                        visited[neighbor.Index] = true;
                        neighbor.Parent = vertex;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
        
        public void DepthFirstSearch(Vertex startVertex, Vertex targetVertex)
        {
            bool[] visited = new bool[vertexCount];

            Stack<Vertex> stack = new Stack<Vertex>();
            List<string> pathList = new List<string>();

            visited[startVertex.Index] = true;
            stack.Push(startVertex);

            while (stack.Count > 0)
            {
                Vertex vertex = stack.Pop();
                pathList.Add(vertex.Name);

                if (vertex.Name.Equals(targetVertex.Name))
                {
                    RetracePath(startVertex, targetVertex);
                    break;
                }

                List<Vertex> neighbors = GetNeighbors(vertex).OrderByDescending(e => e.Distance).ThenBy(e => e.Name).ToList();

                foreach (Vertex neighbor in neighbors)
                {
                    if (!visited[neighbor.Index])
                    {
                        Point point1 = new Point(vertex.X + 3, vertex.Y + 3);
                        Point point2 = new Point(neighbor.X + 3, neighbor.Y + 3);
                        Pen pen = new Pen(Color.Blue, 1);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();

                        visited[neighbor.Index] = true;
                        neighbor.Parent = vertex;
                        stack.Push(neighbor);
                    }
                }
            }
        }

        public void GreedyBestFirstSearch(Vertex startVertex, Vertex targetVertex)
        {
            List<Vertex> openVertices = new List<Vertex>();
            HashSet<Vertex> closedVertices = new HashSet<Vertex>();
            openVertices.Add(startVertex);

            while (openVertices.Count > 0)
            {
                Vertex vertex = openVertices[0];

                for (int i = 1; i < openVertices.Count; i++)
                {
                    if (openVertices[i].FCost < vertex.FCost)
                    {
                        vertex = openVertices[i];
                    }
                }

                openVertices.Remove(vertex);
                closedVertices.Add(vertex);

                if (vertex == targetVertex)
                {
                    RetracePath(startVertex, targetVertex);
                    return;
                }

                foreach (Vertex neighbor in GetNeighbors(vertex))
                {
                    if (closedVertices.Contains(neighbor))
                    {
                        continue;
                    }

                    if (!openVertices.Contains(neighbor))
                    {
                        Point point1 = new Point(vertex.X + 3, vertex.Y + 3);
                        Point point2 = new Point(neighbor.X + 3, neighbor.Y + 3);
                        Pen pen = new Pen(Color.Blue, 1);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();

                        neighbor.HCost = GetDistance(neighbor, targetVertex);
                        neighbor.Parent = vertex;
                        openVertices.Add(neighbor);
                    }
                }
            }
        }

        public void AStarSearch(Vertex startVertex, Vertex targetVertex)
        {
            List<Vertex> openVertices = new List<Vertex>();
            HashSet<Vertex> closedVertices = new HashSet<Vertex>();
            openVertices.Add(startVertex);

            while (openVertices.Count > 0)
            {
                Vertex vertex = openVertices[0];

                for (int i = 1; i < openVertices.Count; i++)
                {
                    if (openVertices[i].FCost <= vertex.FCost)
                    {
                        if (openVertices[i].HCost < vertex.HCost)
                        {
                            vertex = openVertices[i];
                        }
                    }
                }

                openVertices.Remove(vertex);
                closedVertices.Add(vertex);

                if (vertex == targetVertex)
                {
                    RetracePath(startVertex, targetVertex);
                    return;
                }

                List<Vertex> neighbors = GetNeighbors(vertex).OrderByDescending(e => e.Distance).ThenBy(e => e.Name).ToList();

                foreach (Vertex neighbor in neighbors)
                {
                    if (closedVertices.Contains(neighbor))
                    {
                        continue;
                    }

                    int newCostToNeighbour = vertex.GCost + GetDistance(vertex, neighbor);
                    if (newCostToNeighbour < neighbor.GCost || !openVertices.Contains(neighbor))
                    {
                        neighbor.GCost = newCostToNeighbour;
                        neighbor.HCost = GetDistance(neighbor, targetVertex);
                        neighbor.Parent = vertex;

                        if (!openVertices.Contains(neighbor))
                        {
                            Point point1 = new Point(vertex.X + 3, vertex.Y + 3);
                            Point point2 = new Point(neighbor.X + 3, neighbor.Y + 3);
                            Pen pen = new Pen(Color.Blue, 1);
                            Graphics graphics = panelGraph.CreateGraphics();
                            graphics.DrawLine(pen, point1, point2);
                            pen.Dispose();
                            graphics.Dispose();

                            openVertices.Add(neighbor);
                        }
                    }
                }
            }
        }

        public List<Vertex> GetNeighbors(Vertex parentVertex)
        {
            List<Vertex> neighbors = new List<Vertex>();

            for (int i = 0; i < vertexCount; i++)
            {
                if (adjacencyMatrix[parentVertex.Index, i] != 0)
                {
                    Vertex vertex = vertexList[i];
                    vertex.Distance = GetDistance(parentVertex, vertex);
                    neighbors.Add(vertex);
                }
            }

            return neighbors;
        }

        public void RetracePath(Vertex startVertex, Vertex endVertex)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex currentVertex = endVertex;

            while (currentVertex != startVertex)
            {
                path.Add(currentVertex);
                Point point1 = new Point(currentVertex.X + 3, currentVertex.Y + 3);
                Point point2 = new Point(currentVertex.Parent.X + 3, currentVertex.Parent.Y + 3);
                Pen pen = new Pen(Color.Green, 2);
                Graphics graphics = panelGraph.CreateGraphics();
                graphics.DrawLine(pen, point1, point2);
                pen.Dispose();
                graphics.Dispose();
                currentVertex = currentVertex.Parent;
            }
            path.Add(startVertex);
            path.Reverse();
            pathList = path;
        }
    }
}