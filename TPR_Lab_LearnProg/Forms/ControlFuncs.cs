using System.Windows.Forms;
using TPR_Lab_LearnProg.Controls;

public static class ControlFuncs
{
    internal static void DeleteControl(this Form form, string name)
    {
        Control control = form.FindControl(name);
        form.Controls.Remove(control);
    }

    internal static void AddControl(this Form form, string name)
    {
        Control control;
        switch (name)
        {
            case "MainMenuControl":
                control = new MainMenuControl();
                break;
            case "TrainingControl":
                control = new TrainingControl();
                break;
            case "CheckKnowControl":
                control = new CheckKnowControl();
                break;
            default:
                control = null;
                break;
        }
        if (control != null)
        {
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
        }
    }

    internal static Control FindControl(this Control current, string name)
    {
        // Check the parent.
        if (current.Name == name) return current;

        // Recursively search the parent's children.
        foreach (Control control in current.Controls)
        {
            Control found = FindControl(control, name);
            if (found != null) return found;
        }

        // If we still haven't found it, it's not here.
        return null;
    }

    internal static void InitAfterMainMenu(this Form form)
    {
        form.MaximizeBox = true;
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
    }

    internal static void InitForMainMenu(this Form form)
    {
        form.MaximizeBox = false;
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        form.Size = new System.Drawing.Size(495, 300);
    }

    internal static void ChangeScene(string deleteControlName, string addControlName, InitFormType initFormType)
    {
        Form currForm = Form.ActiveForm;
        currForm.DeleteControl(deleteControlName);
        currForm.AddControl(addControlName);
        switch (initFormType)
        {
            case InitFormType.InitAfterMainMenu:
                currForm.InitAfterMainMenu();
                break;
            case InitFormType.InitForMainMenu:
                currForm.InitForMainMenu();
                break;
            default:
                break;
        }
    }
}

internal enum InitFormType
{
    InitAfterMainMenu,
    InitForMainMenu
}