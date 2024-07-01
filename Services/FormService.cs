using FormBackend.Model;
using FormBackend.Services.Context;
namespace FormBackend.Services{
    public class FormService{
        private readonly DataContext _context;
        public FormService(DataContext context){_context = context;}
        public bool NewForm(FormModel newForm) => _context.FormInfo.Add(newForm) != null && _context.SaveChanges() != 0;
        public IEnumerable<FormModel> GetForms() => _context.FormInfo;
        public bool EditForm(FormModel form) => _context.FormInfo.Update((FormModel)_context.FormInfo.Where(Oldform => Oldform.ID == form.ID)) != null && _context.SaveChanges() !=0;
        public bool DeleteForm(int id) => _context.FormInfo.Remove((FormModel)_context.FormInfo.Where(form => form.ID == id)) != null && _context.SaveChanges() !=0;
        public IEnumerable<FormModel> FilterByFirstName() => GetForms().OrderBy(item => item.FirstName);
        public IEnumerable<FormModel> FilterByLastName() => GetForms().OrderBy(item => item.LastName);
    }
}