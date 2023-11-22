using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Xml.Linq;
using RestSharp;


namespace BLL
{
    public enum enumDestanaiton { Replace, notReplace, Exception }
    public class Vertex
    {
        public int Key { get; set; } = int.MaxValue;//המרחק מהצומת הזו לכל צומת 
        public int Parent { get; set; } = -1;//אבא שלה
        public int V { get; set; }//מי היא - הכתובת שלה
        public bool IsProcessed { get; set; }
    }

    public class Points
    {
        public int IdTask { get; set; }
        public string Address { get; set; }
        //בשביל לבדוק האם המשימה עודכנה במסלול או לא
        public bool Status { get; set; } = true;//אם יש משימה שיש בה חריגה ולא יכולה להתבצע הסטטוס שלה ישאר לשקר
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double Length { get; set; }//אם היא נקבעה האורך שלה יאופס ל-0
        public Points(double Length)
        {
            this.Length = Length;
        }


    }
    public class TaskInRouteAlgoritm
    {
        Queue<Points> qPossibleTasks = new Queue<Points>();
        int[][] DistanceMatrix;
        Points home;//כדי לשמור את הכתובת של הבית של הלקוח להכניס אותה לחשיוב המסלול
        Tasks[] ALltasks;
        List<ConstraintsTask> LAllConstraintsReceivedCustomer; //כל אילוצי המשימות שהתקבלו מהלקוח
        Points[] arrPointsConstant;//מערך בשביל המשימות שהלקוח קבע להם שעה
        Queue<Points> QNotPointsConstant;//תור למשימות שהלקוח הכניס ללא שעה קבועה
        DateTime timer;
        public DetailsRouteAndTasksDTO result = new DetailsRouteAndTasksDTO();
        List<Points> routePoints = new List<Points>();
        Route route = new Route();
        List<HoursNotActivityofRouteDTO> hoursNots = new List<HoursNotActivityofRouteDTO>();


        public TaskInRouteAlgoritm(Route route, Tasks[] l, List<ConstraintsTask> lC, bool goBackHome, List<HoursNotActivityofRouteDTO> hoursNots)
        {
            this.hoursNots = hoursNots;
            int j = 0;
            //המערך בגודל כל המשימות שהתקבלו ועוד אחד לבית בהתחלה ואחד לבית בסוף 
            result.createdTasks = new TasksCreatedDTO[goBackHome == true ? (l.Length + 1 + ((int)((DateTime)(route.EndDate) - (DateTime)(route.StartDateToRoute)).TotalDays)) : l.Length + 2];
            result.conflictingTasks = new TasksCreatedDTO[l.Length];
            result.route = Converter.RouteConverter.ConverToRouteDTO(route);
            this.route = route;
            home = createHome(new CustomerDAL().GetCustomer(route.IdCustomers).AddressCustomers, (DateTime)route.StartDateToRoute, (DateTime)route.StartDateToRoute);
            Points home2 = createHome(home.Address, (DateTime)route.EndDate, (DateTime)route.EndDate);
            ALltasks = l;
            timer = (DateTime)route.StartDateToRoute;
            //מיון כל הנקודות הקבועות ולא קבועות על פי שעת ההתחלה
            LAllConstraintsReceivedCustomer = lC;//הצבת רשימת האילוצים בתוך רשימה בראשי שנוכל לגשת אליו מפעולות אחרות
            FillKindTasks();//חלוקה לשני מערכים משימות קבועות ולא קבועות
            ////conflictingTasks();
            arrPointsConstant = arrPointsConstant.OrderBy(x => x.DateStart).ToArray();
            //הכנסתי לתשובה את המשימה הראשונה שהיא הבית

            result.createdTasks[Array.IndexOf(result.createdTasks, default)] = convertToTasksCreatedDTO(arrPointsConstant[0], timer, true);

            ShortedRang();
            SortingPointsDurationPerformance();//מיון המשימות על פי משך זמן הביצועים 

            // עובר כל עוד לא נגמר משימות המסלול וגם עוד לא נגמר הזמן של המסלול
            int i = 0;
            while (CheckingTheTimer(timer, (DateTime)route.EndDate) && i < arrPointsConstant.Length - 1)
            {
                //שאלה ללקוח האם הוא רוצה בסוף כל יום לחזור לבית 
                //אם נגמר היום אז הצבה שהוא יום חדש על פי הימים של המסלול
                if (timer.TimeOfDay > ((DateTime)(route.EndDate)).TimeOfDay)
                {
                    timer = new DateTime(timer.Year, timer.Month, timer.Day + 1, ((DateTime)(route.StartDateToRoute)).TimeOfDay.Hours, ((DateTime)(route.StartDateToRoute)).TimeOfDay.Minutes, ((DateTime)(route.StartDateToRoute)).TimeOfDay.Minutes);
                }
                //משימה קבועה שהיא שגיאה והיא לא יכולה להיקבע אז הסטטוס שלה שווה לפולס 

                if (arrPointsConstant[i + 1].Status == true)
                {
                    if (ifExistsFreeTime(arrPointsConstant[j], arrPointsConstant[i + 1]) == true)
                    {
                        Queue<Points> q1 = new Queue<Points>();
                        qPossibleTasks = new Queue<Points>();
                        qPossibleTasks.Enqueue(arrPointsConstant[i]);
                        q1 = ReducingPossibleTasks(arrPointsConstant[j], arrPointsConstant[i + 1], QNotPointsConstant);
                        if (q1.Count != 0)
                        {
                            concat(qPossibleTasks, q1);
                            //לאתחל מטריצה
                            FillArr(qPossibleTasks);
                            timer = ChoosingTasksAccordingToPrim(DistanceMatrix, arrPointsConstant[i + 1]);
                        }
                        qPossibleTasks = new Queue<Points>();
                    }
                    //הכנסת  המשימה הקבועה לתוך התשובה 
                    result.createdTasks[Array.IndexOf(result.createdTasks, default)] = arrPointsConstant[i + 1].DateStart.AddMinutes(arrPointsConstant[i + 1].Length) < route.EndDate ? convertToTasksCreatedDTO(arrPointsConstant[i + 1], arrPointsConstant[i + 1].DateStart, false) : null;
                    arrPointsConstant[i + 1].Status = result.createdTasks[Array.IndexOf(result.createdTasks, default) - 1] == null ? false : true;
                    timer = result.createdTasks[Array.IndexOf(result.createdTasks, default) - 1].Constraits.DateToConstraint.AddMinutes(result.createdTasks[Array.IndexOf(result.createdTasks, default) - 1].Task.TaskLength);
                    j++;
                }
                i++;
            }
            if (CheckingTheTimer(timer, (DateTime)route.EndDate) && QNotPointsConstant.Count() != 0)
            {
                Points last = arrPointsConstant.LastOrDefault(x => x.Status);
                //צריכה להכניס את המשימה האחרונה שנקבעה 
                qPossibleTasks.Enqueue(last.DateStart.AddMinutes(last.Length).TimeOfDay >= ((DateTime)(route.EndDate)).TimeOfDay ? home : last);
                concat(qPossibleTasks, ReducingPossibleTasks(qPossibleTasks.Peek(), home2, QNotPointsConstant));
                FillArr(qPossibleTasks);
                timer = ChoosingTasksAccordingToPrim(DistanceMatrix, home2);
                timer = result.createdTasks[Array.IndexOf(result.createdTasks, default) - 1].Constraits.DateToConstraint.AddMinutes(result.createdTasks[Array.IndexOf(result.createdTasks, default) - 1].Task.TaskLength);
            }
            //בדקתי את זה בשביל לשים בשגיאה את כל המשימות שלא הצליחו להתבצע
            if (QNotPointsConstant.Count() != 0)
            {
                result.conflictingTasks = result.conflictingTasks.Concat(QNotPointsConstant.Select(x => convertToTasksCreatedDTO(x, new DateTime(), false))).ToArray();
            }
            //אם לא הסתיים לו זמן המסלול וכבר נגמרו המשימות אז להקדים את השעה של החזור לבית לזמן שהגיע השעון של המסלול 
            ///מכניסה שהנקודה האחרונה היא הבית 
            result.createdTasks[Array.IndexOf(result.createdTasks, default)] = convertToTasksCreatedDTO(home2, timer.AddMinutes(DistancsService.GetDystance(result.createdTasks[Array.IndexOf(result.createdTasks, default) - 1].Task.AddressTasks, home2.Address)), true);
            route.EndDate = timer;
            scoreForAlgorithm();
            result.createdTasks = result.createdTasks.Where(x => x != null).ToArray();//להוריד את התאים הרקים במערך
            result.conflictingTasks = result.conflictingTasks.Where(x => x != null).ToArray();//להוריד את התאים הרקים במערך

        }

        public Points createHome(string Address, DateTime DateStart, DateTime DateEnd)
        {
            return new Points(0)
            {
                Address = Address,
                DateStart = DateStart,
                DateEnd = DateEnd,
                Status = true
            };
        }
        public void FillArr(Queue<Points> q)
        {
            DistanceMatrix = new int[q.Count][];
            DistanceMatrix = Enumerable.Range(0, q.Count).Select(i => new int[q.Count]).ToArray();
            for (int i = 0; i < q.Count; i++)
            {
                DistanceMatrix[i] = q.Select(x => DistancsService.GetDystance(q.ElementAt(i).Address, x.Address)).ToArray();
                DistanceMatrix[i][i] = -1;
            }

        }
        //מיון רשימת המשימות שהלקוח קבע על פי משך זמן הביצועים
        public void SortingPointsDurationPerformance()
        {
            try
            {
                int i;
                for (i = 0; i < arrPointsConstant.Length - 1; i++)
                {
                    enumDestanaiton e = Replace(arrPointsConstant[i], arrPointsConstant[i + 1]);//שליחה לפונקציה שבודקת אם צריך החלפה או שיש סתירה
                    if (e == enumDestanaiton.Replace)//בודק אם צריך החלפה בין המששימות 
                    {
                        swap(i, arrPointsConstant);//מחליף בניהם אם צריך החלפה
                        i = CheckTheReplace(i);
                        arrPointsConstant[i + 1].DateStart = arrPointsConstant[i].DateStart.AddMinutes(
                            DistancsService.GetDystance(arrPointsConstant[i].Address, arrPointsConstant[i + 1].Address) + arrPointsConstant[i].Length);
                    }
                    else if (e == enumDestanaiton.Exception)
                    {
                        result.conflictingTasks[Array.IndexOf(result.conflictingTasks, default)] = convertToTasksCreatedDTO(arrPointsConstant[i], LAllConstraintsReceivedCustomer.Find(a => a.IdTasksToCustomers == arrPointsConstant[i].IdTask).DateToConstraint, false);//זה משימה עם חריגה אז לא חשוב התאריך
                        arrPointsConstant[i].Status = false;//הסטטוס הזה אומר על משימות שיש בהם חריגה ולא יכולות להתבצע אם הם יכולות להתבצע מוצב אמת אם יש חריגה מוצב שקר
                    }
                    //מילוי השעות סיום לכל המצבים שצריך
                    if (arrPointsConstant[i + 1].DateEnd < arrPointsConstant[i].DateEnd)
                        arrPointsConstant[i].DateEnd = arrPointsConstant[i + 1].DateStart.AddMinutes(-(DistancsService.GetDystance(arrPointsConstant[i].Address, arrPointsConstant[i + 1].Address)));
                }
                //בשביל לבדוק על המשימה האחרונה כי הפור עוצר 1 קודם
                if (((DateTime)route.StartDateToRoute).AddMinutes(arrPointsConstant[i].Length + DistancsService.GetDystance(home.Address, arrPointsConstant[i].Address)) > (route.EndDate))
                {
                    result.conflictingTasks[Array.IndexOf(result.conflictingTasks, default)] = convertToTasksCreatedDTO(arrPointsConstant[i], new DateTime(), false);
                    arrPointsConstant[i].Status = false;
                }
            }
            catch (Exception)
            {

                ;
            }
        }
        public Queue<Points> ReducingPossibleTasks(Points pOneEnd, Points pTwoStart, Queue<Points> QNotPointsConstant)//מחזיר תור עם המשימות שיכולות להתבצע בין שתי משימות על פי משך הזמן שלהם 
        {
            Queue<Points> qPossibleTasks = new Queue<Points>();
            QNotPointsConstant.Enqueue(new Points(-1));//מכניסה לו משימה ריקה כדי לדעת איפה לעצור
            //לא יכלתי לעשות באלמנט את ולא בפור עד מספר האברים שבתור כי כל לולאה הוא יכול להשתנות ולרדת אז אי אפשר לדעת עד מתי לעצור 
            while (QNotPointsConstant.Peek().Length != -1)
            {
                if (pOneEnd.DateEnd.AddMinutes(IfTaskCompleted(pOneEnd, pTwoStart, QNotPointsConstant.Peek())) < pTwoStart.DateStart)
                {
                    qPossibleTasks.Enqueue(QNotPointsConstant.Dequeue());
                }
                else
                {
                    QNotPointsConstant.Enqueue(QNotPointsConstant.Dequeue());
                }
            }
            //בשביל לשחרר את האיבר האחרון
            QNotPointsConstant.Dequeue();
            return qPossibleTasks;

        }

        public DateTime ChoosingTasksAccordingToPrim(int[][] graph, Points End)
        {
            PriorityQueues<Vertex> queue = new PriorityQueues<Vertex>(true);
            int vertexCount = graph.GetLength(0);
            Vertex[] vertices = new Vertex[vertexCount];
            //עובר על כל הקודקודים ושם להם שהערך הוא אין סוף והאבא הוא נל
            for (int i = 0; i < vertexCount; i++)
                vertices[i] = new Vertex()
                {
                    Key = int.MaxValue,
                    Parent = -1,
                    V = i
                };
            //הנקודה הראשונה היא הבית או הנקודה הנקודה הקבועה שאחריה יש רווח
            vertices[0].Key = 0;
            for (int i = 0; i < vertexCount; i++)
                queue.Enqueue(vertices[i].Key, vertices[i]);
            while (queue.Count > 0)
            {
                Vertex minVertex = queue.Dequeue();
                int u = minVertex.V;
                vertices[u].IsProcessed = true;
                int[] edges = graph[minVertex.V];
                for (int v = 0; v < edges.Length; v++)
                {
                    if (graph[u][v] >= 0 && !vertices[v].IsProcessed && graph[u][v] < vertices[v].Key)
                    {
                        vertices[v].Parent = u;
                        vertices[v].Key = graph[u][v];
                    }
                }
            }
            return SetsTasksInHoursBasedOnDFS(End, vertices.ToList());
        }
        public DateTime SetsTasksInHoursBasedOnDFS(Points End, List<Vertex> allVertex)
        {
            Vertex[] tempVertex = new Vertex[allVertex.Count];
            List<Vertex> childs;
            tempVertex[0] = allVertex[0];
            int k = 1;
            for (int i = 0; i < tempVertex.Length; i++)
            {
                childs = allVertex.Where(x => x.Parent == tempVertex[i].V).ToList();
                for (int j = 0; j < childs.Count; j++, k++)
                {
                    tempVertex[k] = childs[j];
                }
            }
            DateTime timerStart = qPossibleTasks.ElementAt(0).DateEnd;
            DateTime timerEnd = End.DateStart;
            double duration;
            double distancsEnd;
            for (int i = 1; i < tempVertex.Length && timer < route.EndDate; i++)
            {
                //אם יש שעה לא פעילה 
                distancsEnd = DistancsService.GetDystance(qPossibleTasks.ElementAt(tempVertex[i].V).Address, End.Address);
                duration = DistancsService.GetDystance(qPossibleTasks.ElementAt(tempVertex[i - 1].V).Address, qPossibleTasks.ElementAt(tempVertex[i].V).Address) + qPossibleTasks.ElementAt(tempVertex[i].V).Length;
                timer = hoursNots.Find(a => a.StartDate > timer) != null && timer.AddMinutes(distancsEnd + duration) < hoursNots.Find(a => a.StartDate > timer).StartDate ? hoursNots.Find(a => a.StartDate > timer).EndDate : timer;
                //לקדם ליום הבא אם צריך
                timerStart = timerStart.Hour > ((DateTime)(route.EndDate)).Hour ? timerStart.AddDays(1).AddMinutes(duration) : timerStart.AddMinutes(duration);
                if (timerStart.AddMinutes(distancsEnd) < timerEnd)
                {
                    //המשימה נקבעה
                    result.createdTasks[Array.IndexOf(result.createdTasks, default)] = (convertToTasksCreatedDTO(qPossibleTasks.ElementAt(tempVertex[i].V), timerStart.AddMinutes(-qPossibleTasks.ElementAt(tempVertex[i].V).Length), false));

                }
                else
                {
                    QNotPointsConstant.Enqueue(qPossibleTasks.ElementAt(tempVertex[i].V));
                }
            }
            qPossibleTasks = new Queue<Points>();
            return timerStart;
        }
        public void swap(int index, Points[] LP)//פונקציה המחליפה את מיקום הי עם י+1
        {
            Points temp = LP[index];
            LP[index] = LP[index + 1];
            LP[index + 1] = temp;
        }
        public int CheckTheReplace(int i)//אם החלפנו צריך לבדוק את כל הקודמים שאין סתירה בהם
        {
            for (; i > 0 && Replace(arrPointsConstant[i], arrPointsConstant[i - 1]) != enumDestanaiton.notReplace; i--) ;
            return i;
        }
        public bool ifExistsFreeTime(Points p1, Points p2)
        {
            return p1.DateStart.AddMinutes(p1.Length).AddMinutes(DistancsService.GetDystance(p1.Address, p2.Address)) < p2.DateStart;
        }
        public enumDestanaiton Replace(Points p1, Points p2)
        {
            return ((DateTime)route.StartDateToRoute).AddMinutes(p1.Length + DistancsService.GetDystance(home.Address, p1.Address)) > (route.EndDate) || p1.Status == false ? enumDestanaiton.Exception : changeOrderOfExecution(p1, p2) < p2.DateEnd ? enumDestanaiton.notReplace : changeOrderOfExecution(p2, p1) < p1.DateEnd ? enumDestanaiton.Replace : enumDestanaiton.Exception;
        }
        public void ShortedRang()
        {
            arrPointsConstant = arrPointsConstant.Select(x => new Points(0)
            {
                Length = x.Length,
                Address = x.Address,
                IdTask = x.IdTask,
                Status = x.Status,

                DateStart = hoursNots.Find(a => a.StartDate <= x.DateStart && a.EndDate >= x.DateStart) != null ? hoursNots.Find(a => a.StartDate <= x.DateStart && a.EndDate >= x.DateStart).EndDate : x.DateStart,
                DateEnd = hoursNots.Find(a => a.StartDate <= x.DateEnd && a.EndDate <= x.DateEnd) != null ? hoursNots.Find(a => a.StartDate <= x.DateEnd && a.EndDate <= x.DateEnd).StartDate : x.DateEnd
            }).ToArray();
            arrPointsConstant = arrPointsConstant.Select(x => new Points(0)
            {
                Length = x.Length,
                Address = x.Address,
                IdTask = x.IdTask,
                DateEnd = x.DateEnd,
                DateStart = x.DateStart,
                Status = x.DateStart > x.DateEnd ? false : true
            }).ToArray();
        }
        public DateTime changeOrderOfExecution(Points p1, Points p2)
        {
            DateTime t = p1.DateStart;
            t = t.AddMinutes(p1.Length);
            t = t.AddMinutes(DistancsService.GetDystance(p1.Address, p2.Address));
            return t;
        }
        public double IfTaskCompleted(Points PConst, Points pWillConst, Points p)
        {
            double duration = Convert.ToDouble(DistancsService.GetDystance(PConst.Address, p.Address));
            duration += p.Length;
            duration += Convert.ToDouble(DistancsService.GetDystance(p.Address, pWillConst.Address));
            return duration;
        }
        public bool IfExistsTaskNotUpdateTORoute(Points[] arrP)
        {
            return arrP.First(a => a.Status) == null;
        }
        public void concat(Queue<Points> q, Queue<Points> q2)
        {
            while (q2.Count > 0)
            {
                q.Enqueue(q2.Dequeue());
            }
        }
        public bool CheckingTheTimer(DateTime timer, DateTime EndDateToRoute)
        {
            return timer < EndDateToRoute;
        }
        public void FillKindTasks()//מחלק את כל המשימות לשני מבני נתונים על פי הסוג שלהם קבועות ולא קבועות
        {
            arrPointsConstant = new Points[LAllConstraintsReceivedCustomer.Count + 2];//יצירת המערך בשביל המשימות הקבועות

            arrPointsConstant[0] = home;
            QNotPointsConstant = new Queue<Points>();
            //נציב שהמשימה הראשונה היא הבית 
            foreach (ConstraintsTask item in LAllConstraintsReceivedCustomer)
            {
                if (item.DateToConstraint >= DateTime.Now) insertArr(arrPointsConstant, item);//אם יש אילוץ אז המשימה היא קבועה ונציב אותה במערך המשימות הקבועות
                else
                    QNotPointsConstant.Enqueue(convertToPoints(item, ALltasks.FirstOrDefault(a => a.IdTasks == item.IdTasksToCustomers)));//אם אין אילוץ נציב את המשימה בתוך התור של המשימות הלא קבועות
            }
            arrPointsConstant[Array.IndexOf(arrPointsConstant, default)] = createHome(home.Address, (DateTime)route.EndDate, (DateTime)route.EndDate);
            //בשביל לצמצם פערים
            arrPointsConstant = arrPointsConstant[arrPointsConstant.Length - 1] == null ? arrPointsConstant.Take(Array.IndexOf(arrPointsConstant, default)).ToArray() : arrPointsConstant;
        }
        public Points convertToPoints(ConstraintsTask c, Tasks t)
        {
            return new Points(t.TaskLength)
            {
                IdTask = t.IdTasks,
                Address = t.AddressTasks,
                DateStart = c.DateToConstraint,
                DateEnd = c.DateEnd,

            };
        }

        public TasksCreatedDTO convertToTasksCreatedDTO(Points p, DateTime t, bool home)
        {
            return new TasksCreatedDTO()
            {
                Task = new TasksDTO()
                {
                    IdTasks = p.IdTask,
                    NameTasks = home == true ? "בית" : ALltasks.FirstOrDefault(a => a.IdTasks == p.IdTask).NameTasks,
                    AddressTasks = p.Address,
                    TaskLength = p.Length
                },
                Constraits = new ConstraintsTaskDTO()
                {
                    DateEnd = t.AddMinutes(p.Length),
                    DateToConstraint = t,
                },         
            };

        }
        
        public void insertArr(Points[] parr, ConstraintsTask c)
        {
            int firstEmptySlot = Array.IndexOf(parr, default);//המיקום הבא הפנוי במערך
            parr[firstEmptySlot] = new Points(0);
            parr[firstEmptySlot].IdTask = c.IdTasksToCustomers;
            parr[firstEmptySlot].Address = ALltasks.FirstOrDefault(a => a.IdTasks == c.IdTasksToCustomers).AddressTasks;
            parr[firstEmptySlot].DateStart = c.DateToConstraint;
            parr[firstEmptySlot].DateEnd = c.DateEnd;
            parr[firstEmptySlot].Length = ALltasks.FirstOrDefault(a => a.IdTasks == c.IdTasksToCustomers).TaskLength;
        }
        //ציון לאלגוריתם 
        public void scoreForAlgorithm()
        {
            //כמה משימות בתוך האלגוריתם וכמה לא
            result.scoreForAlgorithm = (result.conflictingTasks.Length / ALltasks.Length) * 100;
        }
        //public void TSP(Queue<Points> q, Vertex[] graph, int vertex)
        //{
        //    List<int> unprocessedNeighbors = new List<int>();

        //    // Check each vertex in the graph
        //    for (int i = 0; i < graph.Length; i++)
        //    {
        //        // If the vertex is adjacent to the current vertex and is unprocessed, add it to the list
        //        if (graph[i].V == vertex && !graph[i].IsProcessed)
        //        {
        //            unprocessedNeighbors.Add(i);
        //            graph[i].IsProcessed = true; // Mark the neighbor as processed
        //        }
        //    }
        //    return unprocessedNeighbors;
        //}
    }
}

