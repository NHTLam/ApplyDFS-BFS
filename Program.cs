using static System.Runtime.InteropServices.JavaScript.JSType;

public class Organization
{
    public string Name { set; get; }
    public int ID { set; get; }
    public int TrucThuocID { set; get; }
    public Organization(string name, int iD, int thanhPhanID)
    {
        Name = name;
        ID = iD;
        TrucThuocID = thanhPhanID;
    }

}
internal class Program
{
    public static List<Organization> organization = new List<Organization>() {
        new Organization("Tap doan",    0,        -1),
        new Organization("Cong ty",     1,        0),
        new Organization("Phong Ban",   2,        1),
        new Organization("Nhan vien",   3,        2),
        new Organization("Cong ty 2",   4,        0),
        new Organization("Phong Ban 2", 5,        1),
        new Organization("Phong Ban 3", 6,        4),
        new Organization("Nhan vien 2", 7,        5)
    };

    static void Main(string[] args)
    {
        Console.Write("Nhap so bat ky: ");
        int n = int.Parse(Console.ReadLine());

        Company_Graph company_Graph = new Company_Graph(organization.Count);
        company_Graph.AddCompany(organization[0].ID, organization[1].ID);
        company_Graph.AddCompany(organization[0].ID, organization[4].ID);
        company_Graph.AddCompany(organization[1].ID, organization[2].ID);
        company_Graph.AddCompany(organization[1].ID, organization[5].ID);
        company_Graph.AddCompany(organization[2].ID, organization[3].ID);
        company_Graph.AddCompany(organization[4].ID, organization[6].ID);
        company_Graph.AddCompany(organization[5].ID, organization[7].ID);

        company_Graph.BFS(n, organization);
        Console.WriteLine("----------------------------");
        company_Graph.DFS(n, organization);
    }
}
class Company_Graph
{
    private int _Total_number_of_companies;

    LinkedList<int>[] _List_of_companies;

    public Company_Graph(int Total_number_of_companies)
    {
        _List_of_companies = new LinkedList<int>[Total_number_of_companies];
        for (int i = 0; i < _List_of_companies.Length; i++)
        {
            _List_of_companies[i] = new LinkedList<int>();
        }
        _Total_number_of_companies = Total_number_of_companies;
    }

    public void AddCompany(int holding_company_ID, int SubsidiariesID)
    {
        _List_of_companies[holding_company_ID].AddLast(SubsidiariesID);
    }

    public void BFS(int companyID, List<Organization> organization)
    {
        bool[] CheckedCompany = new bool[_Total_number_of_companies];
        for (int i = 0; i < _Total_number_of_companies; i++)
            CheckedCompany[i] = false;

        LinkedList<int> List_of_companies_waiting_for_inspection = new LinkedList<int>();

        CheckedCompany[companyID] = true;
        List_of_companies_waiting_for_inspection.AddLast(companyID);

        while (List_of_companies_waiting_for_inspection.Any())
        {
            companyID = List_of_companies_waiting_for_inspection.First();
            Console.WriteLine(organization[companyID].Name);
            List_of_companies_waiting_for_inspection.RemoveFirst();

            var list_of_subsidiaries = _List_of_companies[companyID];

            foreach (var val in list_of_subsidiaries)
            {
                if (CheckedCompany[val] == false)
                {
                    CheckedCompany[val] = true;
                    List_of_companies_waiting_for_inspection.AddLast(val);
                }
            }
        }
    }

    public void DFSUtil(int companyID, bool[] CheckedCompany, List<Organization> organization)
    {
        CheckedCompany[companyID] = true;
        Console.WriteLine(organization[companyID].Name);

        var List_of_affiliated_departments = _List_of_companies[companyID];
        foreach (var n in List_of_affiliated_departments)
        {
            if (!CheckedCompany[n])
                DFSUtil(n, CheckedCompany, organization);
        }
    }

    public void DFS(int companyID, List<Organization> organization)
    {
        bool[] CheckedCompany = new bool[_Total_number_of_companies];

        DFSUtil(companyID, CheckedCompany, organization);
    }

}


