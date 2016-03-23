$projects = @('OpenIso8583Net', 'OpenIso8583Net.Tests')

foreach($proj in $projects) {
	echo "Cleaning $proj"
	if(Test-Path $proj/bin) {
		rm -Recurse $proj/bin
	}
	if(Test-Path $proj/obj) {
		rm -Recurse $proj/obj
	}
}
