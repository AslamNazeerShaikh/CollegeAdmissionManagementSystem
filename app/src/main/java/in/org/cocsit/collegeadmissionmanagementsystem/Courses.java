package in.org.cocsit.collegeadmissionmanagementsystem;

import android.app.Dialog;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.cardview.widget.CardView;

import com.daimajia.androidanimations.library.Techniques;
import com.daimajia.androidanimations.library.YoYo;
import com.orhanobut.dialogplus.DialogPlus;
import com.orhanobut.dialogplus.ViewHolder;

public class Courses extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_courses);

        //add hardware acceleration enable
        getWindow().setFlags(
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED,
                WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED);

        //add notch displays cutout mode to short size
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            getWindow().getAttributes().layoutInDisplayCutoutMode = WindowManager.LayoutParams.LAYOUT_IN_DISPLAY_CUTOUT_MODE_SHORT_EDGES;
        }

        ImageView coursesScreenBackButton = findViewById(R.id.courseScreenBackImgBtn);
        coursesScreenBackButton.setOnClickListener(v -> onBackPressed());

        // following are the courses names cardViews
        CardView cardViewBscCS = findViewById(R.id.courseRegBscCS);
        CardView cardViewBscSE = findViewById(R.id.courseRegBscSE);
        CardView cardViewBscNT = findViewById(R.id.courseRegBscNT);
        CardView cardViewMscCS = findViewById(R.id.courseRegMscCS);
        CardView cardViewMscSE = findViewById(R.id.courseRegMscSE);
        CardView cardViewMscSA = findViewById(R.id.courseRegMscSA);
        CardView cardViewMscCM = findViewById(R.id.courseRegMscCM);
        CardView cardViewBCA = findViewById(R.id.courseRegBCA);
        CardView cardViewBscBT = findViewById(R.id.courseRegBscBT);
        CardView cardViewMscBT = findViewById(R.id.courseRegMscBT);
        CardView cardViewBBA = findViewById(R.id.courseRegBBA);
        CardView cardViewMBA = findViewById(R.id.courseRegMBA);

        // following are listeners to the above cardViews
        cardViewBscCS.setOnClickListener(this);
        cardViewBscSE.setOnClickListener(this);
        cardViewBscNT.setOnClickListener(this);
        cardViewMscCS.setOnClickListener(this);
        cardViewMscSE.setOnClickListener(this);
        cardViewMscSA.setOnClickListener(this);
        cardViewMscCM.setOnClickListener(this);
        cardViewBCA.setOnClickListener(this);
        cardViewBscBT.setOnClickListener(this);
        cardViewMscBT.setOnClickListener(this);
        cardViewBBA.setOnClickListener(this);
        cardViewMBA.setOnClickListener(this);


    }

    @Override
    public void onClick(View v) {

        switch (v.getId()){

            case R.id.courseRegBscCS:
                showCoursePopup(v,"B.Sc. Computer Science","Eligibility : 12th Science\nDuration : 3 Years\nSemesters : Total 6\nFees/Year : 17,900.0 Rs."
                                 ,"cbcs_bsc_cs_fy.pdf","cbcs_bsc_cs_sy.pdf","cbcs_bsc_cs_ty.pdf");
                break;
            case R.id.courseRegBscSE:
                showCoursePopup(v,"B.Sc. Software Engineering","Eligibility : 12th Science\nDuration : 3 Years\nSemesters : Total 6\nFees/Year : 17,900.0 Rs."
                                 ,"cbcs_bsc_se_fy.pdf","cbcs_bsc_se_sy.pdf","cbcs_bsc_se_ty.pdf");
                break;
            case R.id.courseRegBscNT:
                showCoursePopup(v,"B.Sc. Network Technology","Eligibility : 12th Science\nDuration : 3 Years\nSemesters : Total 6\nFees/Year : 17,900.0 Rs."
                                 ,"cbcs_bsc_nt_fy.pdf","cbcs_bsc_nt_sy.pdf","cbcs_bsc_nt_ty.pdf");
                break;
            case R.id.courseRegMscCS:
                break;
            case R.id.courseRegMscSE:
                break;
            case R.id.courseRegMscSA:
                break;
            case R.id.courseRegMscCM:
                break;
            case R.id.courseRegBCA:
                break;
            case R.id.courseRegBscBT:
                break;
            case R.id.courseRegMscBT:
                break;
            case R.id.courseRegBBA:
                break;
            case R.id.courseRegMBA:
                break;
        }


    }

    public void showCoursePopup(View view, String myCourseName, String myCourseDetails, String myFyPDF, String mySyPDF, String myTyPDF) {

        final DialogPlus dialog = DialogPlus.newDialog(this)
                .setGravity(Gravity.CENTER)
                .setMargin(50,0,50,0)
                .setContentHolder(new ViewHolder(R.layout.course_popup))
                .setExpanded(false)  // This will enable the expand feature, (similar to android L share dialog)
                .create();

        View holderView = dialog.getHolderView();

        Button openPDFButton1 = holderView.findViewById(R.id.coursePopupPDFBtn1);
        Button openPDFButton2 = holderView.findViewById(R.id.coursePopupPDFBtn2);
        Button openPDFButton3 = holderView.findViewById(R.id.coursePopupPDFBtn3);

        Button registrationButton = holderView.findViewById(R.id.coursePopupRegBtn);
        Button cancelButton = holderView.findViewById(R.id.coursePopupCancelBtn);

        TextView textViewTitle = holderView.findViewById(R.id.coursePopupTextViewTitle);
        TextView textViewSubTitle = holderView.findViewById(R.id.coursePopupTextViewSubTitle);

        textViewTitle.setText(myCourseName);
        textViewSubTitle.setText(myCourseDetails);

        openPDFButton1.setOnClickListener(v -> {
            Intent i = new Intent(this, PDFviewer.class);
            i.putExtra("myPDFname",myFyPDF);
            this.startActivity(i);
        });

        openPDFButton2.setOnClickListener(v -> {
            Intent i = new Intent(this, PDFviewer.class);
            i.putExtra("myPDFname",mySyPDF);
            this.startActivity(i);
        });

        openPDFButton3.setOnClickListener(v -> {
            Intent i = new Intent(this, PDFviewer.class);
            i.putExtra("myPDFname",myTyPDF);
            this.startActivity(i);
        });

        cancelButton.setOnClickListener(v -> dialog.dismiss());

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
}