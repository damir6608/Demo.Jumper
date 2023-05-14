using Demo.Jumper.Window.Create;
using Demo.Jumper.Window.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo.Jumper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private readonly JumperEntities _jumperEntities;
        private const string _allTypes = "Все типы";

        public MainWindow()
        {
            InitializeComponent();
            _jumperEntities = new JumperEntities();
            ListAgents.ItemsSource = _jumperEntities.Agents.ToList();

            var comboboxTypeSortOrderSource = _jumperEntities.Agents.Select(agent => agent.Type).Distinct().ToList();
            comboboxTypeSortOrderSource.Add(_allTypes);
            comboboxTypeSortOrderSource.Reverse();
            CompanyTypeSortOrder.ItemsSource = comboboxTypeSortOrderSource;

            var sortComboboxSource = new string[] { "По возрастанию", "По убыванию" };

            AgentNameSortOrder.ItemsSource = sortComboboxSource;
            SaleSizeSortOrder.ItemsSource = sortComboboxSource;
            PrioritySortOrder.ItemsSource = sortComboboxSource;

        }

        private void CompanyTypeSortOrder_Selected(object sender, RoutedEventArgs e)
        {
            var listSourceAgents = _jumperEntities.Agents.ToList();

            sortByCompanyType(ref listSourceAgents);
            sortByAgentName(ref listSourceAgents);
            sortByPriority(ref listSourceAgents);

            ListAgents.ItemsSource = listSourceAgents;
        }

        private void AgentNameSortOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var agents = ListAgents.ItemsSource as List<Agent>;

            var selectedIndex = AgentNameSortOrder.SelectedIndex;

            agents = selectedIndex == 0 ? agents.OrderBy(s => s.Name).ToList() : selectedIndex == 1 ? agents.OrderByDescending(s => s.Name).ToList() : agents;
            ListAgents.ItemsSource = agents;
        }

        private void SaleSizeSortOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void PrioritySortOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var agents = ListAgents.ItemsSource as List<Agent>;

            var selectedIndex = PrioritySortOrder.SelectedIndex;

            agents = selectedIndex == 0 ? agents.OrderBy(s => s.Priority).ToList() : selectedIndex == 1 ? agents.OrderByDescending(s => s.Priority).ToList() : agents;
            ListAgents.ItemsSource = agents;
        }


        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListAgents.ItemsSource = _jumperEntities.Agents.ToList().FindAll(a => a.Name.Contains(Search.Text));
        }

        private void sortByCompanyType(ref List<Agent> agents)
        {
            var selectedItem = CompanyTypeSortOrder.SelectedItem as string;

            if (selectedItem == _allTypes)
            {
                ListAgents.ItemsSource = _jumperEntities.Agents.ToList();
                return;
            }
            if (selectedItem != null)
                agents = agents.Where(agent => agent.Type == selectedItem).ToList();
        }

        private void sortByAgentName(ref List<Agent> agents)
        {
            var selectedIndex = AgentNameSortOrder.SelectedIndex;

            agents = selectedIndex == 0 ? agents.OrderBy(s => s.Name).ToList() : selectedIndex == 1 ? agents.OrderByDescending(s => s.Name).ToList() : agents;
        }

        private void sortByPriority(ref List<Agent> agents)
        {
            var selectedIndex = PrioritySortOrder.SelectedIndex;

            agents = selectedIndex == 0 ? agents.OrderBy(s => s.Priority).ToList() : selectedIndex == 1 ? agents.OrderByDescending(s => s.Priority).ToList() : agents;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new EditWindow((sender as Button)?.DataContext as Agent).ShowDialog(); 
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var agent = (sender as Button).DataContext as Agent;
            var deletedAgent = _jumperEntities.Agents.Find(agent.Id);
            _jumperEntities.Agents.Remove(deletedAgent);
            _jumperEntities.SaveChanges();

            ListAgents.ItemsSource = _jumperEntities.Agents.ToList();
        }

        private void CreateAgent_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Create(null).ShowDialog();
        }
    }
}
