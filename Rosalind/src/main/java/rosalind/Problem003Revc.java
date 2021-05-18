package rosalind;

import java.util.HashMap;
import java.util.Map;

public class Problem003Revc {

	public static void main(String[] args) {
		String input = "GCCAGTGGTTATGCAGGCATGTCATCTTGGCTATAGTGTCTTCAACTTCCGGCAGGAACTAAAGCGTATCACTACCACGCATAACCGTGCTGCAGGCCTTAAACGTTGACAAGGCCGCGGATTACGTCCAGGATTCAAAGACCTGAGTGCGCCGTAACGTGCGGAGTTTTCTGAGGAGATTGGGAACGATTTTACTATCGACCGGAGATCTGTAGCTCGCTGTACCGGGTCCGTGACTCCTCATCACTGATCTCAACCTGGGCTGAAGACGAGAACCTACTATTAATACATTGAGATTGTACACTCGTGCGCCAACTTACTGTTTACGACAGGGCTTCGTTGCCCCATGTTCATGCAGTTATCCTTTAAACAATCTTGGACGCACGCGCCTGACTGACGAAAAAACAAAGTTCTGTTTTTGTAGCTTGTAGCGGTTAATTCGGCCTAGTTGCGATAGCGATCTAACCTAGTCTCCTTTGCGATCTTGTCCGTGGAATCGTATCCACTGTGAGTCATCGGCGCCTGCTGTAGTCCATCCCCAACCGTCCGATCTTAATTCGTCAGGCTCCTTGCAAAGACCCATAAAACAAGGATGTGCCAGAATATGTCAGCGGACCGTAGCGCACGTACGCCTATTATTCTCTTGAGCCTCAAAAGGCCTCCGTTCGACGGTGAACGAAATAGTTACAAACACTTAACGTAGCTTAGTCGATACGGCGCGTGGTGAGGGATGGACAGGCTCGATGACGAAGCGTTAAACTTCTTAATTGTTAGCCGTATGTCTAATGGGTCGAGGCTGATG";
		StringBuilder outputBuilder = new StringBuilder();
		int inputLength = input.length();
		Map<Character, Character> complementer = new HashMap<>();
		complementer.put('T', 'A');
		complementer.put('C', 'G');
		complementer.put('A', 'T');
		complementer.put('G', 'C');
		for (int i = 0; i < input.length(); i++) {
			outputBuilder.append(complementer.get(input.charAt(inputLength - ( i + 1 ))));
		}
		System.out.println(outputBuilder.toString());
	}

}