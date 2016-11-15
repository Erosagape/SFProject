package com.aerosoft.android.sfdeliveryconfirm;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

public class UpdateStatusActivity extends AppCompatActivity {
    String DeliverNo;
    String Remark;
    String selectedText;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update_status);
        Intent intent=getIntent();
        String uname=intent.getExtras().getString("username");
        if(uname!="")
        {
            String[] arr=uname.split(" ");
            setTitle(arr[1]);
        }
        final Button btnSearch=(Button)findViewById(R.id.btnSubmit);
        btnSearch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                EditText txtDocNo=(EditText)findViewById(R.id.txtDeliveryNo);
                EditText txtRemark=(EditText)findViewById(R.id.txtRemark);
                DeliverNo=txtDocNo.getText().toString();
                Remark=txtRemark.getText().toString();

                ClsUser usr=new ClsUser();

                TextView tvPopup=(TextView)findViewById(R.id.txtDocNo);
                LayoutInflater layoutInflater = (LayoutInflater)getBaseContext().getSystemService(LAYOUT_INFLATER_SERVICE);

                usr.setParameter(layoutInflater, btnSearch);

                AsyncCallWS task=new AsyncCallWS();
                task.SetParameter(usr);
                task.SetParameter("GET_DELIVERY",DeliverNo,Remark,tvPopup);
                task.execute();
            }}
        );
        Button btnUpdate=(Button)findViewById(R.id.btnSave);
        btnUpdate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                RadioGroup opt=(RadioGroup)findViewById(R.id.radioGroup);
                int idselect=opt.getCheckedRadioButtonId();
                RadioButton c1=(RadioButton)findViewById(R.id.radioButton1);
                RadioButton c2=(RadioButton)findViewById(R.id.radioButton2);
                RadioButton c3=(RadioButton)findViewById(R.id.radioButton3);
                RadioButton c4=(RadioButton)findViewById(R.id.radioButton4);
                if(idselect==c1.getId())
                {
                    selectedText="";
                }
                if(idselect==c2.getId())
                {
                    selectedText=c2.getText().toString()+ ":"+ Remark+ ";";
                }
                if(idselect==c3.getId())
                {
                    selectedText=c3.getText().toString()+ ":"+ Remark+ ";";
                }
                if(idselect==c4.getId())
                {
                    selectedText=c4.getText().toString()+ ":"+ Remark+ ";";
                }
                //Toast.makeText(getApplicationContext(),"You select " +selectedText ,Toast.LENGTH_SHORT).show();
                EditText txtDocNo=(EditText)findViewById(R.id.txtDeliveryNo);
                EditText txtRemark=(EditText)findViewById(R.id.txtRemark);
                DeliverNo=txtDocNo.getText().toString();
                Remark=txtRemark.getText().toString();
                if(DeliverNo=="")
                {
                    Toast.makeText(getApplicationContext(),"คุณยังไม่ได้ระบุเลขที่เอกสาร",Toast.LENGTH_SHORT).show();
                }
                else
                {
                    ClsUser usr=new ClsUser();

                    TextView tvPopup=(TextView)findViewById(R.id.txtDocNo);
                    LayoutInflater layoutInflater = (LayoutInflater)getBaseContext().getSystemService(LAYOUT_INFLATER_SERVICE);

                    usr.setParameter(layoutInflater, btnSearch);

                    String valueUpdate="Mark8|" + selectedText;
                    AsyncCallWS task=new AsyncCallWS();
                    task.SetParameter(usr);
                    task.SetParameter("UPDATE_DELIVERY",DeliverNo,valueUpdate,tvPopup);
                    task.execute();
                }
            }
        });
    }
}
