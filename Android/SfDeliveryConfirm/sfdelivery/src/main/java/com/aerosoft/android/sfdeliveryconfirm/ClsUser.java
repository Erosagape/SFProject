package com.aerosoft.android.sfdeliveryconfirm;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.PopupWindow;
import android.widget.TextView;

/**
 * Created by Puttipong on 13/02/2559.
 */
public class ClsUser {
    private String UserID;
    private String UserName;
    private String Password;
    private boolean isLogin=false;
    private AppCompatActivity act;
    private LayoutInflater cont;
    private View vw;
    private Intent intent;
    protected void setActivity(AppCompatActivity app, Intent appintent)
    {
        act=app;
        intent=appintent;
    }
    protected void setUserPassword(String uid,String pwd)
    {
        UserID=uid;
        Password=pwd;
    }
    protected void setUserName(String uname)
    {
        UserName=uname;
    }
    protected void setIsLogin(boolean value)
    {
        isLogin=value;
        if(isLogin==true)
        {
            intent.putExtra("userid",UserID);
            intent.putExtra("password",Password);
            intent.putExtra("username",UserName);
            act.startActivity(intent);
        }
    }
    protected void setParameter(LayoutInflater layout,View view)
    {
        cont=layout;
        vw=view;
    }
    protected void showPopup(String msg)
    {
        View popupView = cont.inflate(R.layout.popup_layout, null);
        final PopupWindow popupWindow = new PopupWindow(popupView, ViewGroup.LayoutParams.WRAP_CONTENT,ViewGroup.LayoutParams.WRAP_CONTENT);

        TextView tv=(TextView)popupView.findViewById(R.id.txtDocNo);
        tv.setText(msg);

        Button btnDismiss = (Button)popupView.findViewById(R.id.dismiss);
        btnDismiss.setOnClickListener(new Button.OnClickListener(){
                                          @Override
                                          public void onClick(View v) {
                  // TODO Auto-generated method stub
                  popupWindow.dismiss();
           }}
        );
        popupWindow.showAsDropDown(vw, 50, -30);
        popupWindow.setFocusable(true);
        popupWindow.update();
    }
}
