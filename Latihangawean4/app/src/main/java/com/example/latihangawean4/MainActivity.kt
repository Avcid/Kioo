package com.example.latihangawean4

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.activity.result.IntentSenderRequest
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.lifecycle.lifecycleScope
import com.example.latihangawean4.databinding.ActivityMainBinding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.MainScope
import kotlinx.coroutines.launch
import org.json.JSONObject
import java.net.HttpURLConnection
import java.net.URL

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.tvToRegister.setOnClickListener {
            val intent = Intent(this, MainActivity2::class.java)
            startActivity(intent)
        }

        binding.btnLogin.setOnClickListener {
            val email = binding.etEmail.text.toString()
            val password = binding.etPassword.text.toString()

            if (email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Semua field harus di isi", Toast.LENGTH_SHORT).show()
            }

            lifecycleScope.launch(Dispatchers.IO){
                val request = URL("http://10.0.2.2:5000/api/auth").openConnection() as HttpURLConnection
                request.requestMethod = "POST"
                request.setRequestProperty("Content-Type", "application/json")

                val dataUser = JSONObject().apply{
                    put("password", password)
                    put("email", email)
                }

                val os = request.getOutputStream()
                os.write(dataUser.toString().toByteArray())

                val responseCode = request.responseCode

                if (responseCode == HttpURLConnection.HTTP_OK) {
                    val intent = Intent(this@MainActivity, MainActivity3::class.java)
                    startActivity(intent)
                    finish()
                } else {
                    runOnUiThread {
                        Toast.makeText(this@MainActivity, "Login failed!", Toast.LENGTH_SHORT).show()
                    }
                }
            }
        }
    }
}