package in.org.cocsit.collegeadmissionmanagementsystem;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.WindowManager;

import com.daimajia.androidanimations.library.Techniques;
import com.daimajia.androidanimations.library.YoYo;

public class SplashScreen extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash_screen);

        //add hardware acceleration enable
        getWindow().setFlags(
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED,
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED);

        //add notch displays cutout mode to short size
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P){
            getWindow().getAttributes().layoutInDisplayCutoutMode = WindowManager.LayoutParams.LAYOUT_IN_DISPLAY_CUTOUT_MODE_SHORT_EDGES;
        }

        YoYo.with(Techniques.FadeInUp)
                .duration(2000)
                .repeat(0)
                .playOn(findViewById(R.id.SplashScreenLogo));

        YoYo.with(Techniques.FadeIn)
                .duration(2000)
                .repeat(0)
                .playOn(findViewById(R.id.SplashScreenTitle));

        YoYo.with(Techniques.FadeInUp)
                .duration(2000)
                .repeat(0)
                .playOn(findViewById(R.id.SplashScreenSubTitle));

        Handler handler = new Handler(); //handel the post delay method with intent to call next activity
        handler.postDelayed(() -> {

                startActivity(new Intent(SplashScreen.this,MainActivity.class));
                finish();    //permanently closes the previous activity
        },3000);  //value seconds //current value is 2sec

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