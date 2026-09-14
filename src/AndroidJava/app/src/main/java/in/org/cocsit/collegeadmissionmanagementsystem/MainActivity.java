package in.org.cocsit.collegeadmissionmanagementsystem;

import android.app.Dialog;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.cardview.widget.CardView;

import com.daimajia.androidanimations.library.Techniques;
import com.daimajia.androidanimations.library.YoYo;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //add hardware acceleration enable
        getWindow().setFlags(
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED,
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED);

        //add notch displays cutout mode to short size
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            getWindow().getAttributes().layoutInDisplayCutoutMode = WindowManager.LayoutParams.LAYOUT_IN_DISPLAY_CUTOUT_MODE_SHORT_EDGES;
        }

        CardView MainScreenRegistrationCard = findViewById(R.id.mainScreenRegistrationCard);
        CardView MainScreenWalkthroughCard = findViewById(R.id.mainScreenWalkthroughCard);

        MainScreenRegistrationCard.setOnClickListener(this); //added onclick listener
        MainScreenWalkthroughCard.setOnClickListener(this);

        ImageView showDialogButton = findViewById(R.id.mainScreenBackImgBtn);
        showDialogButton.setOnClickListener(view -> showExitDialog());

    }

    public void showExitDialog() {

        YoYo.with(Techniques.Shake)
                .duration(2000)
                .repeat(0)
                .playOn(findViewById(R.id.mainScreenBackImgBtn));

        Dialog dialog = new Dialog(MainActivity.this);
        dialog.setContentView(R.layout.exit_alert_dialog);

        Button acceptButton = dialog.findViewById(R.id.acceptButton);
        Button cancelButton = dialog.findViewById(R.id.cancelButton);

        acceptButton.setOnClickListener(view -> {
            dialog.dismiss();
            Log.e("Exit_App_Yes", "onClick: accept button");

            Handler handler = new Handler(); //handel the post delay method with intent to call next activity
            handler.postDelayed(() -> {
                MainActivity.this.finish();
                System.exit(0);
            },2000);  //value seconds //current value is 2sec

            Toast.makeText(getApplicationContext(), "You choose Yes action",Toast.LENGTH_SHORT).show();
        });

        cancelButton.setOnClickListener(view -> {
            Log.e("Exit_App_No", "onClick: cancel button" );
            dialog.dismiss();
            Toast.makeText(getApplicationContext(), "You choose No action",Toast.LENGTH_SHORT).show();
        });

        dialog.show();
    }

    @Override
    public void onClick(View v) {

        switch (v.getId()) {
            case R.id.mainScreenRegistrationCard:
                Intent i = new Intent(this, Courses.class);
                startActivity(i);
                break;
            case R.id.mainScreenWalkthroughCard:
                Intent j = new Intent(this, SplashScreen.class);
                startActivity(j);
                break;
        }
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
}