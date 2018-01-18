using Starcounter;

namespace KitchenSink
{
    partial class SortablePage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            SortablePage.PersonsElementJson person;
            person = this.Persons.Add();
            person.Name = "Rocky";

            person = this.Persons.Add();
            person.Name = "Tigger";

            person = this.Persons.Add();
            person.Name = "Bella";

            person = this.Persons.Add();
            person.Name = "Anik";

            person = this.Persons.Add();
            person.Name = "Amya";
    }
}