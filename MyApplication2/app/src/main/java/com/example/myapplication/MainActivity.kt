package com.example.myapplication

import android.app.DatePickerDialog
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.myapplication.databinding.ActivityMainBinding
import java.util.Calendar

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val sharedPref = getSharedPreferences("USER_PREF", Context.MODE_PRIVATE)
        if (sharedPref.getBoolean("IS_LOGIN", false)) {
            startActivity(Intent(this, MainActivity2::class.java))
            finish()
            return
        }

        binding.etTanggalLahir.setOnClickListener {
            val cal = Calendar.getInstance()
            val year = cal.get(Calendar.YEAR)
            val month = cal.get(Calendar.MONTH)
            val day = cal.get(Calendar.DAY_OF_MONTH)

            val datePicker = DatePickerDialog(
                this,
                { _, selectedYear, selectedMonth, selectedDay ->
                    val mm = (selectedMonth + 1).toString().padStart(2, '0')
                    val dd = selectedDay.toString().padStart(2, '0')
                    binding.etTanggalLahir.setText("$dd-$mm-$selectedYear")
                },
                year, month, day
            )
            datePicker.show()
        }

        binding.btnLogin.setOnClickListener {
            val username = binding.etUsername.text.toString().trim()
            val password = binding.etPassword.text.toString().trim()
            val tglLahir = binding.etTanggalLahir.text.toString().trim()

            if (username.isNotEmpty() && password.isNotEmpty() && tglLahir.isNotEmpty()) {
                sharedPref.edit()
                    .putString("NAMA_USER", username)
                    .putString("TGL_LAHIR", tglLahir)
                    .putBoolean("IS_LOGIN", true)
                    .apply()

                startActivity(Intent(this, MainActivity2::class.java))
                finish()
            } else {
                Toast.makeText(this, "Username, Password, dan Tanggal lahir tidak boleh kosong", Toast.LENGTH_SHORT).show()
            }
        }
    }
}
