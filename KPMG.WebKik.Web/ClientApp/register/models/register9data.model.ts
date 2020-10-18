import { ContryCodeViewModel } from './country-code.model';

export
	class Register9DataViewModel {
	//	public CountryCode CountryName { get; set; }
	public Register9Id: number;
	public Id: number;

	public StockholderName: string;
	public CountryCodeId: number;
	public CountryCode: ContryCodeViewModel;
	public LastYearDividendPaymentYear: number;


	public LastYearDividendPaymentData: Date;
	public LastYearDividendSum: number;
	public CurrentYearTransitionalDividendPaymentData: Date;
	public CurrentYearTransitionalDividendSum: number;
	public CurrentYearDividendPaymentData: Date;
	public CurrentYearDividendSum: number;

	public Summary: number;
	public SummaryYear: number;
	}