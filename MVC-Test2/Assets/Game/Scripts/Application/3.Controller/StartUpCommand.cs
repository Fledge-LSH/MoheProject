using System;

public class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型
        RegisterModel(new M_Bag());
        //注册视图
        RegisterView(new V_Bag());

    }
}
