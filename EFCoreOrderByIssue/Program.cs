// See https://aka.ms/new-console-template for more information
using EFOrderByIssue;
MyContext myContext = new MyContext();
var entities = myContext.Set<Student>();
var list = File.ReadAllLines("text.txt").Select(s => Guid.Parse(s));
var ordered = entities.Where(s => list.Contains(s.Guid)).OrderByItems(s => s.Guid, list);

if (ordered.Any())
{

}

Console.ReadLine();