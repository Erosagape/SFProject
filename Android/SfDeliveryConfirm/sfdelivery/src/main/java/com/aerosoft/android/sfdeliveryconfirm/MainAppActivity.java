package com.aerosoft.android.sfdeliveryconfirm;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class MainAppActivity extends AppCompatActivity {
    TextView txtStatus;
    EditText txtUser;
    EditText txtPassword;
    Button btn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_app);

        txtStatus=(TextView)findViewById(R.id.txtStatus);
        txtUser=(EditText)findViewById(R.id.txtUser);
        txtPassword=(EditText)findViewById(R.id.txtPassword);

        btn=(Button)findViewById(R.id.btnLogin);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                txtStatus.setText("Connecting...");
                ClsUser user=new ClsUser();

                Intent frmUpd=new Intent(MainAppActivity.this,UpdateStatusActivity.class);
                user.setActivity(MainAppActivity.this,frmUpd);

                AsyncCallWS task = new AsyncCallWS();
                task.SetParameter(user);
                task.SetParameter("LOGIN", txtUser.getText().toString(), txtPassword.getText().toString(), txtStatus);
                task.execute();
            }
        });
    }
}
