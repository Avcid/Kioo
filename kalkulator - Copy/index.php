<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Kalkulator</title>
</head>
<body>
    <h2>Kalklator</h2>
    <form action="" method="POST"> 
    
    <input type="number" name="angka1" placeholder="Angka pertama" required>

    <select name="operasi" id="">
        <option value="tambah">+</option>
        <option value="kurang">-</option>
        <option value="kali">*</option>
        <option value="bagi">/</option>
    </select>

    <input type="number" name="angka2" placeholder="Angka kedua" required>
    
    <button type="submit" name="hitung">Hitung</button>
</form>

    <?php 
    if (isset($_POST['hitung'])){
        $a1 = $_POST['angka1'];
        $a2 = $_POST['angka2'];
        $ops = $_POST['operasi'];
        $hasil = 0;

        if ($ops == "tambah"){
            $hasil = $a1 + $a2;
        }elseif ($ops == "kurang"){
            $hasil = $a1 - $a2;
        }elseif ($ops == "kali"){
            $hasil = $a1 * $a2;
        }elseif ($ops == "bagi"){
            $hasil = ($a2 != 0) ? $a1 / $a2 : "Tak terhingga";
        }

        echo "<h3>Hasil: $hasil</h3>";
    }
    ?>
</body>
</html>