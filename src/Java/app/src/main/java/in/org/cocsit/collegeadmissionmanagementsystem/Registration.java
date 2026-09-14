package in.org.cocsit.collegeadmissionmanagementsystem;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Dialog;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.daimajia.androidanimations.library.Techniques;
import com.daimajia.androidanimations.library.YoYo;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import okhttp3.WebSocket;
import okhttp3.WebSocketListener;
import okio.ByteString;

public class Registration extends AppCompatActivity {

    private EditText fName, mName, lName, my10passYear, my12passYear, myUGpassYear;
    private EditText my10marks, my12marks, myUGmarks, mBirthDay, mBirthMonth, mBirthYear;
    private EditText mBloodGroup, mNationality, mGender, mCourseName, mPriNo, mSecNo;
    private EditText mPerAdd, mCurAdd;

    private Button mNextBtn, mCancelBtn;

    private WebSocket webSocket;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registration);

        instantiateWebSocket("192.168.2.5:9999");

        //add hardware acceleration enable
        getWindow().setFlags(
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED,
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED);

        //add notch displays cutout mode to short size
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            getWindow().getAttributes().layoutInDisplayCutoutMode = WindowManager.LayoutParams.LAYOUT_IN_DISPLAY_CUTOUT_MODE_SHORT_EDGES;
        }

        fName = findViewById(R.id.RegWinFname);
        mName = findViewById(R.id.RegWinMname);
        lName = findViewById(R.id.RegWinLname);

        my10marks = findViewById(R.id.RegWin10thMarks);
        my12marks = findViewById(R.id.RegWin12thMarks);
        myUGmarks = findViewById(R.id.RegWinUGmarks);

        my10passYear = findViewById(R.id.RegWin10thPassYear);
        my12passYear = findViewById(R.id.RegWin12thPassYear);
        myUGpassYear = findViewById(R.id.RegWinUGpassYear);

        mBirthDay = findViewById(R.id.RegWinBirthDay);
        mBirthMonth = findViewById(R.id.RegWinBirthMonth);
        mBirthYear = findViewById(R.id.RegWinBirthYear);

        mBloodGroup = findViewById(R.id.RegWinBloodGroup);
        mNationality = findViewById(R.id.RegWinNationality);
        mGender = findViewById(R.id.RegWinGender);

        mPriNo = findViewById(R.id.RegWinPrimaryPhone);
        mSecNo = findViewById(R.id.RegWinSecondaryPhone);

        mCourseName = findViewById(R.id.RegWinCourseName);

        mPerAdd = findViewById(R.id.RegWinPermanentAddress);
        mCurAdd = findViewById(R.id.RegWinCurrentAddress);

        mNextBtn = findViewById(R.id.coursePopupRegBtn);
        mCancelBtn = findViewById(R.id.coursePopupCancelBtn);


        mNextBtn.setOnClickListener(v -> {
            showNextRegDialog();
        });

        mCancelBtn.setOnClickListener(v -> {
            showCancelRegDialog();
        });

    }


    public void showCancelRegDialog() {

        Dialog dialog = new Dialog(Registration.this);
        dialog.setContentView(R.layout.exit_alert_dialog);

        Button acceptButton = dialog.findViewById(R.id.acceptButton);
        Button cancelButton = dialog.findViewById(R.id.cancelButton);
        TextView textView = dialog.findViewById(R.id.popupAlertTextView);

        acceptButton.setText("CONTINUE");
        cancelButton.setText("DISMISS");

        textView.setText("College of Computer Science\n& Information Technology\n\nOnline Admission Management System\n\nThis App is made possible by the Android open source project and other open source software. Made with ❤ in COCSIT. Thanks for using this app.\n\nWarning: You will lose all the data you entered !\nAre sure you want to go back ?");

        acceptButton.setOnClickListener(view -> {
            onBackPressed();
            Log.e("Exit_App_Yes", "onClick: accept button");

            Handler handler = new Handler(); //handel the post delay method with intent to call next activity
            handler.postDelayed(this::onBackPressed,500);  //value seconds //current half millisecond
            Toast.makeText(getApplicationContext(), "You choose Yes action",Toast.LENGTH_SHORT).show();
        });

        cancelButton.setOnClickListener(view -> {
            Log.e("Exit_App_No", "onClick: cancel button" );
            dialog.dismiss();
            Toast.makeText(getApplicationContext(), "You choose No action",Toast.LENGTH_SHORT).show();
        });

        dialog.show();
    }

    public void showNextRegDialog() {

        Dialog dialog = new Dialog(Registration.this);
        dialog.setContentView(R.layout.exit_alert_dialog);

        Button acceptButton = dialog.findViewById(R.id.acceptButton);
        Button cancelButton = dialog.findViewById(R.id.cancelButton);
        TextView textView = dialog.findViewById(R.id.popupAlertTextView);

        acceptButton.setText("REGISTER");
        cancelButton.setText("DISMISS");

        textView.setText("College of Computer Science\n& Information Technology\n\nOnline Admission Management System\n\nThis App is made possible by the Android open source project and other open source software. Made with ❤ in COCSIT. Thanks for using this app.\n\nWarning: By going ahead you will get registered !\nAre sure you want to register ?");

        acceptButton.setOnClickListener(view -> {
            sendDataToSever();
            Log.e("Exit_App_Yes", "onClick: accept button");

            Handler handler = new Handler(); //handel the post delay method with intent to call next activity
            handler.postDelayed(this::onBackPressed,500);  //value seconds //current half millisecond
            Toast.makeText(getApplicationContext(), "You choose Yes action",Toast.LENGTH_SHORT).show();
        });

        cancelButton.setOnClickListener(view -> {
            Log.e("Exit_App_No", "onClick: cancel button" );
            dialog.dismiss();
            Toast.makeText(getApplicationContext(), "You choose No action",Toast.LENGTH_SHORT).show();
        });

        dialog.show();
    }

    // Enables regular immersive mode or fullscreen mode
    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);
        View decorView = getWindow().getDecorView();
        if (hasFocus) {
            decorView.setSystemUiVisibility(View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY
                    | View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                    | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                    | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                    | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
                    | View.SYSTEM_UI_FLAG_FULLSCREEN);
        }
    }

    private void sendDataToSever(){

        //"omkesh$bhivajirao$munde$73$59$77$2015$2016$2020$01$05$1998$A$Indian$male$6363636$7373737$BscCS$present xyz adress$curreunt Xyz adress"

        String mySendString = fName.getText().toString() + "$" +
                              mName.getText().toString() + "$" +
                              lName.getText().toString() + "$" +

                my10marks.getText().toString() + "$" +
                my12marks.getText().toString() + "$" +
                myUGmarks.getText().toString() + "$" +

                my10passYear.getText().toString() + "$" +
                my12passYear.getText().toString() + "$" +
                myUGpassYear.getText().toString() + "$" +

                mBirthDay.getText().toString() + "$" +
                mBirthMonth.getText().toString() + "$" +
                mBirthYear.getText().toString() + "$" +

                mBloodGroup.getText().toString() + "$" +
                mNationality.getText().toString() + "$" +
                mGender.getText().toString() + "$" +

                mPriNo.getText().toString() + "$" +
                mSecNo.getText().toString() + "$" +

                mCourseName.getText().toString() + "$" +
                mPerAdd.getText().toString() + "$" +
                mCurAdd.getText().toString()  ;

        webSocket.send(mySendString);

    }

    public class SocketListener extends WebSocketListener {

        public Registration activity;
        public SocketListener(Registration activity) {
            this.activity = activity;
        }

        @Override
        public void onClosed(@NotNull WebSocket webSocket, int code, @NotNull String reason) {
            super.onClosed(webSocket, code, reason);
        }

        @Override
        public void onClosing(@NotNull WebSocket webSocket, int code, @NotNull String reason) {
            super.onClosing(webSocket, code, reason);
        }

        @Override
        public void onFailure(@NotNull WebSocket webSocket, @NotNull Throwable t, @Nullable Response response) {
            super.onFailure(webSocket, t, response);
        }

        @Override
        public void onMessage(@NotNull WebSocket webSocket, @NotNull String text) {
            super.onMessage(webSocket, text);
        }

        @Override
        public void onMessage(@NotNull WebSocket webSocket, @NotNull ByteString bytes) {
            super.onMessage(webSocket, bytes);
        }

        @Override
        public void onOpen(@NotNull WebSocket webSocket, @NotNull Response response) {
            super.onOpen(webSocket, response);
        }
    }

    private void instantiateWebSocket(String ipaddress) {

        OkHttpClient client = new OkHttpClient();

        //replace x.x.x.x with your machine's IP Address
        Request request = new Request.Builder().url("ws://"+ipaddress).build();
        SocketListener socketListener = new SocketListener(this);
        webSocket = client.newWebSocket(request, socketListener);
    }
}