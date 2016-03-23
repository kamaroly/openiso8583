namespace OpenIso8583Net.Emv
{
    /// <summary>
    /// EMV Tag
    /// </summary>
    public enum Tag
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The issuer_id_num.
        /// </summary>
        issuer_id_num = 0x42, 

        /// <summary>
        /// The aid.
        /// </summary>
        aid = 0x4f, 

        /// <summary>
        /// The appl_label.
        /// </summary>
        appl_label = 0x50, 

        /// <summary>
        /// The cmd_to_perform.
        /// </summary>
        cmd_to_perform = 0x52, 

        /// <summary>
        /// The track 2_eq_data.
        /// </summary>
        track2_eq_data = 0x57, 

        /// <summary>
        /// The appl_pan.
        /// </summary>
        appl_pan = 0x5a, 

        /// <summary>
        /// The cardholder_name.
        /// </summary>
        cardholder_name = 0x5f20, 

        /// <summary>
        /// The expiry_date.
        /// </summary>
        expiry_date = 0x5f24, 

        /// <summary>
        /// The effect_date.
        /// </summary>
        effect_date = 0x5f25, 

        /// <summary>
        /// The issuer_country_code.
        /// </summary>
        issuer_country_code = 0x5f28, 

        /// <summary>
        /// The trans_curcy_code.
        /// </summary>
        trans_curcy_code = 0x5f2a, 

        /// <summary>
        /// The lang_preference.
        /// </summary>
        lang_preference = 0x5f2d, 

        /// <summary>
        /// The service_code.
        /// </summary>
        service_code = 0x5f30, 

        /// <summary>
        /// The appl_pan_seqnum.
        /// </summary>
        appl_pan_seqnum = 0x5f34, 

        /// <summary>
        /// The trans_curr_exp.
        /// </summary>
        trans_curr_exp = 0x5f36, 

        /// <summary>
        /// The issuer_url.
        /// </summary>
        issuer_url = 0x5f50, 

        /// <summary>
        /// The int_bank_acc_no.
        /// </summary>
        int_bank_acc_no = 0x5f53, 

        /// <summary>
        /// The bank_id_code.
        /// </summary>
        bank_id_code = 0x5f54, 

        /// <summary>
        /// The issuer_country_code 2.
        /// </summary>
        issuer_country_code2 = 0x5f55, 

        /// <summary>
        /// The issuer_country_code 3.
        /// </summary>
        issuer_country_code3 = 0x5f56, 

        /// <summary>
        /// The account_type.
        /// </summary>
        account_type = 0x5f57, 

        /// <summary>
        /// The appl_templ_61.
        /// </summary>
        appl_templ_61 = 0x61, 

        /// <summary>
        /// The fci_templ_6 f.
        /// </summary>
        fci_templ_6f = 0x6f, 

        /// <summary>
        /// The isuer_scrpt_templ_71.
        /// </summary>
        isuer_scrpt_templ_71 = 0x71, 

        /// <summary>
        /// The isuer_scrpt_templ_72.
        /// </summary>
        isuer_scrpt_templ_72 = 0x72, 

        /// <summary>
        /// The dir_discr_templ_73.
        /// </summary>
        dir_discr_templ_73 = 0x73, 

        /// <summary>
        /// The respmsg_fmt 1_templ_77.
        /// </summary>
        respmsg_fmt1_templ_77 = 0x77, 

        /// <summary>
        /// The respmsg_fmt 2_templ_80.
        /// </summary>
        respmsg_fmt2_templ_80 = 0x80, 

        /// <summary>
        /// The amount_auth.
        /// </summary>
        amount_auth = 0x81, 

        /// <summary>
        /// The appl_intchg_profile.
        /// </summary>
        appl_intchg_profile = 0x82, 

        /// <summary>
        /// The cmd_template.
        /// </summary>
        cmd_template = 0x83, 

        /// <summary>
        /// The df_name.
        /// </summary>
        df_name = 0x84, 

        /// <summary>
        /// The issuer_script_cmd.
        /// </summary>
        issuer_script_cmd = 0x86, 

        /// <summary>
        /// The appl_priority_ind.
        /// </summary>
        appl_priority_ind = 0x87, 

        /// <summary>
        /// The sfi.
        /// </summary>
        sfi = 0x88, 

        /// <summary>
        /// The auth_code.
        /// </summary>
        auth_code = 0x89, 

        /// <summary>
        /// The auth_resp_code.
        /// </summary>
        auth_resp_code = 0x8a, 

        /// <summary>
        /// The cdol 1.
        /// </summary>
        cdol1 = 0x8c, 

        /// <summary>
        /// The cdol 2.
        /// </summary>
        cdol2 = 0x8d, 

        /// <summary>
        /// The cvm_list.
        /// </summary>
        cvm_list = 0x8e, 

        /// <summary>
        /// The auth_pubkey_index.
        /// </summary>
        auth_pubkey_index = 0x8f, 

        /// <summary>
        /// The iss_pubkey_cert.
        /// </summary>
        iss_pubkey_cert = 0x90, 

        /// <summary>
        /// The iss_auth_data.
        /// </summary>
        iss_auth_data = 0x91, 

        /// <summary>
        /// The iss_pubkey_rem.
        /// </summary>
        iss_pubkey_rem = 0x92, 

        /// <summary>
        /// The signed_sad.
        /// </summary>
        signed_sad = 0x93, 

        /// <summary>
        /// The afl.
        /// </summary>
        afl = 0x94, 

        /// <summary>
        /// The tvr.
        /// </summary>
        tvr = 0x95, 

        /// <summary>
        /// The tdol.
        /// </summary>
        tdol = 0x97, 

        /// <summary>
        /// The tc_hash.
        /// </summary>
        tc_hash = 0x98, 

        /// <summary>
        /// The pin_data.
        /// </summary>
        pin_data = 0x99, 

        /// <summary>
        /// The tran_date.
        /// </summary>
        tran_date = 0x9a, 

        /// <summary>
        /// The tsi.
        /// </summary>
        tsi = 0x9b, 

        /// <summary>
        /// The tran_type.
        /// </summary>
        tran_type = 0x9c, 

        /// <summary>
        /// The ddf_name.
        /// </summary>
        ddf_name = 0x9d, 

        /// <summary>
        /// The acq_id.
        /// </summary>
        acq_id = 0x9f01, 

        /// <summary>
        /// The amt_auth_num.
        /// </summary>
        amt_auth_num = 0x9f02, 

        /// <summary>
        /// The amt_other_num.
        /// </summary>
        amt_other_num = 0x9f03, 

        /// <summary>
        /// The amt_other_bin.
        /// </summary>
        amt_other_bin = 0x9f04, 

        /// <summary>
        /// The appl_disc_data.
        /// </summary>
        appl_disc_data = 0x9f05, 

        /// <summary>
        /// The appl_id.
        /// </summary>
        appl_id = 0x9f06, 

        /// <summary>
        /// The appl_use_cntrl.
        /// </summary>
        appl_use_cntrl = 0x9f07, 

        /// <summary>
        /// The app_ver_num.
        /// </summary>
        app_ver_num = 0x9f08, 

        /// <summary>
        /// The term_ver_num.
        /// </summary>
        term_ver_num = 0x9f09, 

        /// <summary>
        /// The crdhldrname_ext.
        /// </summary>
        crdhldrname_ext = 0x9f0b, 

        /// <summary>
        /// The iac_default.
        /// </summary>
        iac_default = 0x9f0d, 

        /// <summary>
        /// The iac_denial.
        /// </summary>
        iac_denial = 0x9f0e, 

        /// <summary>
        /// The iac_online.
        /// </summary>
        iac_online = 0x9f0f, 

        /// <summary>
        /// The issuer_app_data.
        /// </summary>
        issuer_app_data = 0x9f10, 

        /// <summary>
        /// The isssuer_code_tbl.
        /// </summary>
        isssuer_code_tbl = 0x9f11, 

        /// <summary>
        /// The appl_pre_name.
        /// </summary>
        appl_pre_name = 0x9f12, 

        /// <summary>
        /// The last_online_atc.
        /// </summary>
        last_online_atc = 0x9f13, 

        /// <summary>
        /// The lc_offline_lmt.
        /// </summary>
        lc_offline_lmt = 0x9f14, 

        /// <summary>
        /// The mer_cat_code.
        /// </summary>
        mer_cat_code = 0x9f15, 

        /// <summary>
        /// The mer_id.
        /// </summary>
        mer_id = 0x9f16, 

        /// <summary>
        /// The pin_try_counter.
        /// </summary>
        pin_try_counter = 0x9f17, 

        /// <summary>
        /// The issuer_script_id.
        /// </summary>
        issuer_script_id = 0x9f18, 

        /// <summary>
        /// The term_county_code.
        /// </summary>
        term_county_code = 0x9f1a, 

        /// <summary>
        /// The term_floor_limit.
        /// </summary>
        term_floor_limit = 0x9f1b, 

        /// <summary>
        /// The temr_id.
        /// </summary>
        temr_id = 0x9f1c, 

        /// <summary>
        /// The term_riskmgmt_data.
        /// </summary>
        term_riskmgmt_data = 0x9f1d, 

        /// <summary>
        /// The ifd_ser_num.
        /// </summary>
        ifd_ser_num = 0x9f1e, 

        /// <summary>
        /// The track 1_disc_data.
        /// </summary>
        track1_disc_data = 0x9f1f, 

        /// <summary>
        /// The tran_time.
        /// </summary>
        tran_time = 0x9f21, 

        /// <summary>
        /// The appl_cryptogram.
        /// </summary>
        appl_cryptogram = 0x9f26, 

        /// <summary>
        /// The crypt_info_data.
        /// </summary>
        crypt_info_data = 0x9f27, 

        /// <summary>
        /// The term_cap.
        /// </summary>
        term_cap = 0x9f33, 

        /// <summary>
        /// The cvm_results.
        /// </summary>
        cvm_results = 0x9f34, 

        /// <summary>
        /// The atc.
        /// </summary>
        atc = 0x9f36, 

        /// <summary>
        /// The unpred_num.
        /// </summary>
        unpred_num = 0x9f37, 

        /// <summary>
        /// The term_adtnal_cap.
        /// </summary>
        term_adtnal_cap = 0x9f40, 

        /// <summary>
        /// The trans_seq_counter.
        /// </summary>
        trans_seq_counter = 0x9f41, 

        /// <summary>
        /// The data_auth_code.
        /// </summary>
        data_auth_code = 0x9f45, 

        /// <summary>
        /// The icc_dynamic_num.
        /// </summary>
        icc_dynamic_num = 0x9f4c, 

        /// <summary>
        /// The icc_request.
        /// </summary>
        icc_request = 0xff20, 

        /// <summary>
        /// The icc_response.
        /// </summary>
        icc_response = 0xff21
        // ReSharper restore InconsistentNaming
    }
}